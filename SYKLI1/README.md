# PONG klooni
README AVAILABLE ONLY IN FINNISH, contact me if you need help with translating this.
- Tehty keväällä 2021, tekijänä **Aaron Isoaho**
- Tehty osana *TIES476 Peliteknologia* -kurssin suoritusta
- Tehty *Phaser 3* frameworkilla


Toimiva versio löytynee osoitteesta [JYUn users -hakemistostani](http://users.jyu.fi/~aakaneis/TIES476/pong/) ja lähdekoodi löytynee [githubistani](http://github.com).

-------------
## Ajaaksesi omalla koneella koodia
Jotta voit ajaa omalla koneella koodia, täytyy sinun varmistaa, että sinulta löytyy lokaali web-palvelin asennus. (kts. [Getting Started](http://phaser.io/tutorials/getting-started)) 

Vaihtoehtoisesti voit pelata peliä [selaimellasi](http://users.jyu.fi/~aakaneis/TIES476/pong/index.html).
-------------
## Prosessi
Tässä kuvataan henkilökohtaista oppimisprosessiani itselleni uuden pelimoottorin kanssa.
### Miten tein valinnan?
Valitsin pelimoottoriksi Phaser 3 viitekehyksen, sillä itselläni on tulossa velhosykli koskien HTML5:sta alustana. Ajattelin yhdistää täten velhoilun harjoittelun, sekä syklin tehtävän suorittamisen tekemällä syklin 1 tehtävän Phaser -viitekehyksellä.

Tehtävänannossa vaadittiin, että toteutetaan pongin perus pelimekaniikka mutta siihen tulisi lisätä vähintään yksi oma pelimekaniikka lisää. Tässä versiossa on Pongiin lisätty painovoima ja keskiverkko, joka kääntää pongin periaatteessa kuvaamaan pingistä sivulta päin (toisin kuin perusPong, joka periaatteessa kuvaa pingistä ylhäältä päin). 
### Miten tutustuin pelimoottoriin
Pelimoottoriin tutustuin ensin Phaserin omalla [Making your first Phaser 3 game](http://phaser.io/tutorials/making-your-first-phaser-3-game/part1) -sivulla. Noudatin kyseisiä ohjeita, jotta opin hieman alkeita viitekehyksestä ja sen käyttämisestä. 

Sillä taustallani on web-sovellusten ja web-käyttöliittymien kurssi, muotoilin phaserin oman tutoriaalin dokumentteja vastaamaan enemmän web-sovellusten kansiorakennetta. 
### Mihin haasteisiin törmäsin ja miten ne ratkaisin
| Haaste | Ratkaisu |
|--------|----------|
| Fonttikoon muuttaminen pisteiden laskua varten | Dokumentaation lukeminen, sitten päätyminen font: "..." -ratkaisuun, kun erikseen laitetut "fontFamily", "fontSize" ym. eivät toimineet. |
| Pallo liikutti mailaa törmätessään | Mailoille ja verkoille lisätty ```XX.body.immovable = True``` |