# SYKLI5: Proseduraalinen generointi, ohjelmointitehtävä

Tehtävänannon löydät [TIMistä](https://tim.jyu.fi/view/kurssit/tie/peliteknologia/syklit-2021/sykli-5-proseduraalinen-generointi/viikkotehtava).

Pohjana käytetty [tätä](https://github.com/Lumi-Orkidea/GenerointiTehtava), mutta siihen on lisätty [tämä](https://github.com/Vahv1/ai_tehtava/tree/master/Assets/PathFinding) kansio.

## Ajaaksesi
Lataa tämä kansio (```TIES476/SYKLI5/``` ) ja avaa se käyttäen Unity Hubin ```Add```-painiketta.

## Suoritus

| Vaatimus | Status | Ratkaisu |
| --- | --- | --- |
| Kenttä on aina ratkaistavissa | TEHTY | Ratkaistavuus tehty hyödyntämällä syklissä 4 esiintynyttä pathfinding koodia, sekä tarkistuksia (esimerkiksi että maalipiste on tarpeeksi kaukana pelaajasta, tarpeeksi tässä tapauksessa tarkoittaa puolen kentän leveyttä) |
| Kenttään spawnataan vihollisia, jotka ovat seinän takana | TEHTY | Raycastilla tarkastetaan, että vihollisen sijainnin ja pelaajan sijainnin sivulla on seinä. |
| Aloitus- ja lopetuskohdat satunnaistetaan | TEHTY | Pelaajan satunnaistaminen riveillä 92 - 103, vihollisten satunnaistaminen riveillä 155 - 175. |

| Rajoitukset | Status | Missä huomioidaan |
| --- | --- | --- |
| Satunnaistettujen aloitus- ja lopetussijaintien etäisyys oltava puolen kentän mitta | TEHTY | Maalin sijaintia etsiessä tarkastetaan, että etäisyys pelaajan sijainnista maaliin on yli puolet kentän leveydestä (rivi 127). |
| Kenttä on ratkaistavissa | TEHTY | Katso vaatimus-taulusta ensimmäinen rivi. |
| Seinien ja kulkulaattojen suhde oltava noin 1:1 | TEHTY | Generaattori laskee (min+max)/2 keskiarvon ja sitä hyödyntämällä sijoittaa seiniä ja lattioita kenttään. (rivit 53 - 88 ) |
| Pelaaja voi liikkua vain yhden ruudun kerrallaan vaaka- tai pystysuoraan. | TEHTY | Tämä huomioidaan, sillä koodissa hyödynnetään syklin 4 pathfinding skriptejä, joissa oli sama oletus. |

## Mitä toteutin

Aikaa tähän on käytetty noin 5 tuntia.

Toteutetut ominaisuudet:

- Itse säädettävä kartan koko sekä vihollisten lukumäärä
- Diamond square -kohina-algoritmi, jonka pohjalta kenttä satunnaistetaan
- Kohinasta lasketaan keskiarvo ja kaikki ruudut jotka ovat alle keskiarvon, ovat seiniä (ja muut ovat ruohikkoa)
- Generaattori sijoittaa pelaajan sattumanvaraiseen pisteeseen kentällä
- Generaattori yrittää sijoittaa loppupisteen siten, että
  - Etäisyys pelaajaan on yli puolet leveydestä sekä
  - On löydettävissä reitti pelaajasta lopetuspisteeseen
- Jos maalia ei voida asettaa, generaattori purkaa luomansa asiat ja käynnistää itsensä uudelleen.
- Generaattori sijoittaa kentälle N määrän vihollisia, joiden sijainnin ja pelaajan välillä on vähintäänkin yksi seinä.

### Vihollisten spawnaamisen korjaus

Aiemmassa versiossa viholliset pystyivät spawnaamaan pelaajan eteen. Kävi ilmi, että ensinäkin raycastilla castattiin ikuinen säde, joka jatkoi matkaansa minkä kerkesi.
Tämän lisäksi raycast huomioi KAIKEN kentältä, joten siirtämällä muut kuin viholliset ignore raycast -layerille, pitäisi raycastin nyt tunnistaa vain seinät. 
