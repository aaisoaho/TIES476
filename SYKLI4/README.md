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

*Minkä arvosanan saisit?*

Todennäköisesti arvosanan 4.

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
| Tekoäly ei jää jumiin seiniin eikä jää pyörimään loputtomia silmukoita pelikentällä (vinkki: jos ei muuta keksi niin random) | Tehty? |
| Tekoäly osaa lyödä vihollista sellaisen sattuessa eteen. | Tehty |
| Koodi on kommentoitu selkeästi (mitä ominaisuuksia tekoälylle mikäkin koodin kohta toteuttaa) | Tehty |
| Kunnollinen raportti palautettu | TYÖNALLA |

Lisäpisteiden vaatimukset:

| Vaatimus | Status |
| --- | --- |
| Tekoäly “etsii” vihollisia jollain tapaa ja hakeutuu niiden lähelle | Tehty |
| Tekoäly osaa ottaa huomioon vihollisten asennon eli yrittää joissain tapauksissa lyödä niitä kylkeen tai selkään | - |
| Tekoäly osaa ottaa jollain tavalla huomioon vihollisten HPn (esim. hakeutuu vahingoittuneiden vihollisten lähelle) | Tehty |
| Tekoäly tunnistaa jollain tavalla jääneensä jumiin/silmukkaan ja selviää siitä fiksummalla logiikalla kuin kääntymällä randomsuuntiin ja liikkumalla | - |
| Tekoäly osaa välttää vihollisia jollain tapaa (esim. vaihtaa suuntaa jos vihollinen on perässä) | - |
| Mikä tahansa muu “älykäs” ominaisuus, mikä tulee mieleen eikä sitä tässä erikseen mainita. Esitelkää ominaisuus selvästi ja kommentoikaa se koodiin, jotta tarkastajatkin ymmärtävät mitä olette tehneet. | - |
| Tekoäly voittaa maanantaina järjestettävän mahtikamppailun | - |
