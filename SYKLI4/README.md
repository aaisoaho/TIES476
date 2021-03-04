# Sykli 4 - pelitekoäly
Tarkoituksena tehtävässä oli kehittää mahdollisimman hyvä pelitekoäly yksinkertaista peliä varten.

Tehtävän suoritus: 
1. Nappaa projekti [reposta](https://github.com/Vahv1/ai_tehtava)
2. Muokkaa AI_Sukunimi -skriptin nimi vastaamaan omaa
3. Muokkaa skriptiä siten, että käytät PlayerControllerInterfacen tarjoamia metodeja.

Tarkempi tehtävänanto löytyy [linkin](https://tim.jyu.fi/view/kurssit/tie/peliteknologia/syklit-2021/sykli-4-pelitekoaly/tehtava) takaa.

## Tehtävän teko
*Miten meni?*

Tämä oli suhteellisen tuskastuttava tehtävä. Yhteensä aikaa kului 8 tuntia. Hauskin osuus oli hahmotella ensin paperilla miten tekoälyn tulisi toimia. 

*Missä oli vaikeuksia?*

- Suunnan katsominen
- Koodin selkeys

*Toteutetut ominaisuudet*

- Tekoäly valitsee joka vuoro jonkin toiminnon (Lyö, Liiku eteen, Käänny Vasemmalle, Käänny Oikealle)
- Tekoäly ottaa kohteekseen vihollisen (Valinta: Luodaan lista pienimmän HP:n vihollisista, josta etsitään lähin kohde)
  - Tämä kattaa HP:n huomioimisen
- Tekoäly koettaa suunnistaa kohteen luo
  - Kattaa lisävaatimuksen etsimisestä ja lähelle hakeutumisesta
- Tekoälyllä on alkeellinen keino suunnitella (pystyy valitsemaan ennalta seuraavan siirron)

*Minkä arvosanan saisit?*

Todennäköisesti arvosanan 5.

### Tarkempi kuvaus tekoälyn toiminnallisuudesta

- Tekoälyllä on kolme muistiin liittyvää muuttujaa: ```pakota_eteen```, ```edellinen``` ja ```seuraava```.
  - ```pakota_eteen``` on ```bool``` -tyyppinen, jonka ```true```-arvolla tekoäly priorisoi eteenpäin liikkumista
  - ```edellinen``` on ```int``` -tyyppinen muuttuja, jonka arvo määräytyy edellisen liikkeen mukaan
  - ```seuraava``` on ```int``` -tyyppinen muuttuja, johon tallennetaan tekoälyn päättämän liikkeen mukainen arvo.
- ```edellinen``` ja ```seuraava``` -muuttujien arvojen tulkinta:
  - 0 = lyö
  - 1 = liiku eteenpäin
  - 2 = käänny vasempaan päin
  - 3 = käänny oikeaan päin
  - idle -toimintoa pidin sellaisena, jota ei kannattaisi tehdä, koska sillä "tuhlaa" vuoronsa ja altistaa itsensä turhaan toisten lyömisille.

```DecideNextMove()``` -funktiossa pyörii seuraavanlainen tilakone:

1. Jos edessä on vihollinen, lyödään vihollista (eli asetetaan ```seuraava```-muuttujalle arvo 0), muuten
2. Jos ```pakota_eteen``` on ```true``` ja eteenpäin pääsee (eli edessä ei ole seinää), niin liikutaan eteenpäin (eli asetetaan ```seuraava```-muuttujalle arvo 1), muuten
3. Suunnistetaan lähimpään matalimman HP-pisteen omaavaan viholliseen (eli ```suunnistaUhriin(...)``` funktio päättää muuttujalle ```seuraava``` jonkin arvon, jolla yritetään päästä lähemmäs kohdetta)

Tämän tilakoneen jälkeen siirrytään tilakoneeseen, joka seuraa ```seuraava```-muuttujan arvoa ja päättää sen mukaan liikkeen.

- Kääntymisiä vastaavissa arvoissa tarkastetaan, mikä oli edellinen siirto, siltä varalta, että tekoäly ei kääntyisi edestakaisin peräkkäisillä siirroilla.

```private void suunnistaUhriin(Vector2 uhri)``` funktio tarkastaa suunnan tekoälystä asetettuun pisteeseen ```uhri``` ja päättää sen mukaan kutsutaanko:

- ```suunnistaOikealle(Vector2 uhri)```
- ```suunnistaVasemmalle(Vector2 uhri)```
- ```suunnistaYlos(Vector2 uhri)```
- ```suunnistaAlas(Vector2 uhri)```

Nämä kaikki neljä funktiota sisältävät tilakoneen, jossa tarkastetaan botin suunta, ja suunnan mukaan päätetään mitä tehdään.

- Jos suunta on eteenpäin (eli jos esimerkiksi ```suunnistaYlos(Vector2 uhri)``` -funktion tilanteessa botti on kääntyneenä ylöspäin), koetetaan liikkua eteenpäin, paitsi jos edessä on seinä, niin käännytään sattumanvaraisesti oikeaan tai vasempaan
  - Sattumanvaraisesti sen vuoksi, että botti ei näe seiniä, joten kiertotie voi olla oikealla tai vasemmalla
  - Botti myös tässä tilanteessa asettaa seuraavan siirron prioriteetiksi liikkua eteenpäin, jotta se ei jäisi kääntyilemään vain edestakaisin.
- Jos suunta on täysin vastakkainen, botti valitsee sattumanvaraisesti kääntyykö se vasemman vai oikean kautta ympäri, taas koska botti ei näe seiniä, joten molemmat ovat sille yhtä hyviä vaihtoehtoja
- Jos botti katsoo 90 asteen kulmaan halutusta, se kääntyy "oikeinpäin".

```uhri```-argumentin arvoksi annetaan funktiokutsu ```etsiUhri()```-funktioon, joka 

1. Hakee kaikkien vihollisten sijainnit
2. Etsii sijaintilistalta matalimman HP-määrän
3. Listaa kaikki matalimmalla HP-määrällä olevat sijainnit
4. Etsii näistä sijainneista lähimmän ja palauttaa sen.
## Vaatimukset
1 pisteen vaatimukset:
| Vaatimus | Status |
| --- | --- |
| Tekoäly osaa liikkua pelimaailmassa jäämättä jumiin ensimmäiseen vastaan tulevaan seinään | Tehty | 
| Tekoäly osaa lyödä vihollista sellaisen sattuessa eteen. | Tehty |
| Koodissa on ainakin yksi kommentti | Tehty |
| Raporttina palautettu jotain | Tehty |

2 pisteen vaatimukset:
| Vaatimus | Status |
| --- | --- |
| Tekoäly ei jää jumiin seiniin eikä jää pyörimään loputtomia silmukoita pelikentällä (vinkki: jos ei muuta keksi niin random) | Tehty |
| Tekoäly osaa lyödä vihollista sellaisen sattuessa eteen. | Tehty |
| Koodi on kommentoitu selkeästi (mitä ominaisuuksia tekoälylle mikäkin koodin kohta toteuttaa) | Tehty |
| Kunnollinen raportti palautettu | Tehty |

Lisäpisteiden vaatimukset:

| Vaatimus | Status |
| --- | --- |
| Tekoäly “etsii” vihollisia jollain tapaa ja hakeutuu niiden lähelle | Tehty |
| Tekoäly osaa ottaa huomioon vihollisten asennon eli yrittää joissain tapauksissa lyödä niitä kylkeen tai selkään | - |
| Tekoäly osaa ottaa jollain tavalla huomioon vihollisten HPn (esim. hakeutuu vahingoittuneiden vihollisten lähelle) | Tehty |
| Tekoäly tunnistaa jollain tavalla jääneensä jumiin/silmukkaan ja selviää siitä fiksummalla logiikalla kuin kääntymällä randomsuuntiin ja liikkumalla | Tehty |
| Tekoäly osaa välttää vihollisia jollain tapaa (esim. vaihtaa suuntaa jos vihollinen on perässä) | - |
| Mikä tahansa muu “älykäs” ominaisuus, mikä tulee mieleen eikä sitä tässä erikseen mainita. Esitelkää ominaisuus selvästi ja kommentoikaa se koodiin, jotta tarkastajatkin ymmärtävät mitä olette tehneet. | Tekoäly muistaa edellisen siirtonsa ja kykenee priorisoimaan eteenpäin liikkumista (mutta ykkösprioriteettina on kuitenkin vihollisen kohdattaessa lyöminen) |
| Tekoäly voittaa maanantaina järjestettävän mahtikamppailun | - |
