# Sykli 2:n tehtävä
Tehtävässä tarkoituksena suunnitella ja toteuttaa jokin mobiilialustalta löytyvää ominaisuutta hyödyntävä prototyyppi. Prototyypin ei tarvitse olla "proof of concept"-tasoa laajempi. Alustana tulee käyttää Androidia.

Tehtävä pisteytetään vastaavasti:
1. Idea/kuvaus mobiilisovellukselle palautettuna kirjallisesti.
2. Sovelluksessa käytetään jotain mobiilista löytyvää.
3. Palautettu lähdekoodi.
4. Palautettu APK.
5. Lähdekoodi dokumentoitu fiksusti.

## Meta

| Pisteytys | Suoritus | Eteneminen |
| --- | --- | --- |
| 1 = Idea/kuvaus mobiilisovellukselle palautettuna kirjallisesti. | TOTEUTETTU  | Idea kuvaillaan tämän dokumentin seuraavassa luvussa *Prototyypin idea*. |
| 2 = Sovelluksessa käytetään jotain mobiilista löytyvää esim gyroskooppi demossa. | TOTEUTETTU | Luvussa *Prototyypin idea* kuvaillaan mobiililaitteiden *landscape* ja *portrait* näkymien hyödyntämistä pelimekaniikan konseptissa |
| 3 = Palautettu lähdekoodi | TYÖN ALLA | Valitaan sopiva moottori, jolla toteuttaa konsepti alla kuvattu konsepti. |
| 4 = Palautettu APK | - | - |
| 5 = Lähdekoodi dokumentoitu fiksusti. | - | - |
| Bonus: +1 piste = Ruudunkaappaus projektista iOS-laitteella | EI TOTEUTETA | - |

## Prototyypin idea
Prototyypin tavoitteena on esitellä mahdollista *Puzzle*-elementin toteutusta. Kyseenomainen elementti hyödyntää puhelimissa käytettäviä *landscape* ja *portait* näkymiä. Tämä konsepti pysyy tässä toteutuksessa vielä konseptitasolla, eikä ota kantaa pelin genrestä tai muista pelikohtaisista ominaisuuksista, kuten peliobjektin pysyvyydestä kummassakin tasossa/näkymässä.

Kyseenomaisilla näkymillä on tarkoitus leikitellä siten, että molemmat sisältäisivät erillisen tason/ulottuvuuden. Käytännössä siis *portrait* näkymässä näkyy eri "tasohyppelykenttä" kuin *landscape* näkymässä, ja täten voidaan toteuttaa puzzle-elementti, jossa pelistä on kerralla näkyvissä periaatteessa puolet kentästä. 

Proof of Conceptissa (tästedes tässä dokumentissa viitataan proof of conceptiin vain lyhenteellä PoC) tulisi siis näyttää, että pelissä on mahdollista:
1. tunnistaa, onko puhelin pystyssä (*portrait*) vai vaakatasossa (*landscape*)
2. pelin elementtejä voidaan piirtää pelinäkymä riippuen siitä, ollaanko pysty- vai vaakatasossa.

Tätä PoC:ta voitaisiin laajentaa myöhemmin toteuttamalla jokin ominaisuus, jolla tunnistaa pelaajan sijainti kummassakin "ulottuvuudessa"/tasossa. Tällainen ankkurointitapa tosin on kyseenomaisen PoCn kannalta vielä epäolennainen.