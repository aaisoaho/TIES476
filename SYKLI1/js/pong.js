// Alustetaan peli
var config = {
    type: Phaser.AUTO,
    width: 600,
    height: 400,
    physics: {
        default: 'arcade'
    },
    scene: { // Emme tarvitse kuin yhden skenen, jossa tarvitaan preload, create ja update funktioita
        preload: preload,
        create: create,
        update: update
    }
};
var game = new Phaser.Game(config);

// Alustetaan muuttujat pelin pisteyttämiselle
// scoreTextPlayer ja scoreTextAI tulee sisältämään
// viitteen, jonka avulla piirretään pisteet näytölle.
let scorePlayer = 0;
let scoreAI     = 0;
let scoreTextPlayer;
let scoreTextAI;

// Heitetään noppaa pallon alkusuunnasta. 
// Tämän olisi voinut kyllä muuttaa funktioksi.
let dice = Math.random();
let velocityX;
let velocityY;
if (dice <=0.5) {
    velocityX=-100;
    if (dice <= 0.25) {
        velocityY=-300;
    } else {
        velocityY=300;
    }
} else {
    velocityX=100;
    if (dice <= 0.75) {
        velocityY=-300;
    } else {
        velocityY=300;
    }
}

// function reset ()
// ------------------------------------------------------------------
//  Resetoidaan pelitilanne, eli palautetaan mailat ja pallo 
//  niiden alkutilaan. Pallolle arvotaan liikerata.
function reset () 
{
    dice = Math.random();
    if (dice <=0.5) {
        velocityX=-100;
        if (dice <= 0.25) {
            velocityY=-300;
        } else {
            velocityY=300;
        }
    } else {
        velocityX=100;
        if (dice <= 0.75) {
            velocityY=-300;
        } else {
            velocityY=300;
        }
    }

    player.x    = 60;
    player.y    = 100;
    aiBat.x     = 540;
    aiBat.y     = 100;
    ball.x      = 300;
    ball.y      = 200;
    ball.setVelocityX(velocityX);
    ball.setVelocityY(velocityY);
}

//  function preload ()
// ------------------------------------------------------------------
//  Ladataan assetit.
function preload ()
{
    this.load.image('bg', 'assets/bg.png');
    this.load.image('ball', 'assets/ball.png');
    this.load.image('bat', 'assets/bat.png');
}

//  function create ()
// ------------------------------------------------------------------
//  Sceneä luodessa ajettava funktio
function create ()
{
    // Lisätään näkymään taustakuva
    this.add.image(300, 200, 'bg');
    // Ladataan kontrollit/inputit
    cursor = this.input.keyboard.createCursorKeys();

    // Lisätään näkymään pelaajan ja tekoälyn maila, sekä 
    // staattinen maila keskiverkoksi.
    player = this.physics.add.sprite(60,100,'bat');
    aiBat = this.physics.add.sprite(600-60,100,'bat');
    net = this.physics.add.sprite((600)/2,350,'bat');
    // Pelaajan ja tekoälyn mailojen ei tulisi päästä kentän ulkopuolelle.
    player.setCollideWorldBounds(true);
    aiBat.setCollideWorldBounds(true);
    // Ettei pallo liikuta mailoja törmätessään.
    player.body.immovable = true;
    aiBat.body.immovable = true;
    net.body.immovable = true;

    // Lisätään pallo, säädetään sen alkunopeus arvotun mukaiseksi.
    ball = this.physics.add.sprite(300,200,'ball');
    ball.setVelocityX(velocityX);
    ball.setVelocityY(velocityY);
    // Estetään palloa pääsemästä kentän ulkopuolelle ja laitetaan se pomppimaan. Lisätään pallolle painovoima
    ball.setCollideWorldBounds(true);
    ball.setBounce(1,1);
    ball.body.gravity.y = 300;
    // Lisätään törmäyksien käsittelyt mailoihin ja verkkoihin.
    this.physics.add.collider(ball,player,hitBat,null,this);
    this.physics.add.collider(ball,aiBat,hitBat,null,this);
    this.physics.add.collider(ball,net,hitBat,null,this);

    // Lisätään pisteenlasku. Style määrittää tekstin tyylin
    let style = {font: "bold 40px Courier New", fill: '#eed2bc' };
    scoreTextPlayer = this.add.text(30, 10, '0', style);
    scoreTextAI = this.add.text(570, 10, '0', style);
}

//  function update ()
// ------------------------------------------------------------------
//  Päivitysfunktio. Täällä tapahtuu kaikki 
//  muutokset pelin tilanteeseen.
function update () 
{
    // Pelaajan liikkuminen
    if (cursor.up.isDown)
    {
        player.setVelocityY(-200);
    } 
    else if (cursor.down.isDown && !player.body.touching.bottom) {
        // Kunhan pallo ei ole puristumassa mailan alle niin voidaan liikkua.
        player.setVelocityY(200);
    }
    else
    {
        player.setVelocityY(0);
    }
    // Pelaajan jälkeen päivitetään tekoälyn liikkumista
    updateAI();
    // Tarkastetaan pallon sijainti
    if (ball.x < 50)
    { // Pallo on pelaajan mailan 'takana'
        scoreAI += 1;
        scoreTextAI.setText(scoreAI);
        reset();
    } else if (ball.x > 550)
    { // Pallo on tekoälyn mailan takana
        scorePlayer += 1;
        scoreTextPlayer.setText(scorePlayer);
        reset();
    }
}

//  function hitBat(ball,bat)
// ------------------------------------------------------------------
//  ball    = pallo, joka törmää
//  bat     = maila, johon törmätään
// ------------------------------------------------------------------
//  Törmäyksen käsittelyyn tarkoitettu funktio. 
function hitBat(ball,bat)
{
    if (velocityX<0) { // pallo liikkuu vasemmalle
        velocityX=velocityX-10; // kiihdytetään liikettä
        if (ball.body.touching.left || ball.body.touching.right) // Jos pallo osuu vasemmalla tai oikealla kyljellä, se tarkoittaa että ollaan kimpoamassa.
            velocityX = velocityX*(-1); // käännetään liike oikealle
    } else { // muutoin liikutaan oikealle
        velocityX=velocityX+10; // kiihdytetään
        if (ball.body.touching.left || ball.body.touching.right) // Jos pallo osuu vasemmalla tai oikealla kyljellä, se tarkoittaa että ollaan kimpoamassa.
            velocityX = velocityX*(-1); // käännetään vasemmalle
    }
    ball.setVelocityX(velocityX); // Päivitetään liikkeen tieto pallolle
    if (velocityY<0) 
    { 
        velocityY = velocityY*(-1);
        ball.setVelocityY(velocityY);
    }
}

//  function updateAI()
// ------------------------------------------------------------------
//  Mailan alkeellinen tekoäly, maila seuraa pallon korkeutta.
function updateAI()
{
    // Katsotaan pallon ja mailan korkeusero ja sen mukaan 
    // liikutetaan mailaa
    let y_diff = ball.y - aiBat.y;
    if (y_diff < 0) {
        // Pallo on mailan yläpuolella
        aiBat.setVelocityY(-200);
    } else if (y_diff > 0 && !aiBat.body.touching.bottom) {
        // Pallo on mailan alapuolella
        // Kunhan pallo ei ole puristumassa mailan alle niin voidaan liikkua.
        aiBat.setVelocityY(200);
    } else {
        // Pallo on tasan siellä missä tekoälykin
        aiBat.setVelocityY(0);
    }
}