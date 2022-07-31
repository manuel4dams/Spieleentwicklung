# Spieleentwicklung Zombie

## Feedback offiziell 29.07.2022

* ~~1 @ Michi - Einstellungsmenu (Gamma, Sound)~~
* 0 - ~~Spieler sieht man bisschen schlecht -> Lichtquelle geben~~
  1 - ~~Headlight!~~ 
* 1 - ~~Eventuell Vignette~~
* ~~1 @ Michi - Ganz dunkles Licht von unten nach oben stahlen mit sehr dunklem Grün (fill Light)~~
  Hat nur so mittel geklappt (mittelhelles Blau)
* 1 - ~~Letzter Raum kann man durch die Türe glitchen (Türe löschen)~~
* 0 - Kein Physikalischen Sprung. Eine Kraft für hoch, langsamerer Fall
  0? - Animation ist komisch mit dem Bein
* 0 - Wenn Kiste schräg steht, dann kann man zu hoch springen
* 0 - ~~Flammenwerfer sollte aus gleichem Tank ziehen und unterschiedlich verbrauchen~~
  0 - ~~Oder aber separate Waffen draus machen~~
  1 - ~~Vielleicht eine Mine?~~ Falle, Laserschranke ~~(Mine ersetzt großen Flammenwerfer!)~~
* 0 - Barrel lässt einen über Zombies glitchen
* 1 - ~~Nebel bei den normalen Schluchten anheben~~
* 1 - ~~Mündungen sind oberhalb der Kiste -> Kiste hoch skalieren~~
* 0 @ Michi - Spielen sollte nicht von Kisten ab-bouncen (fühlt sich bisschen strange an)
  Passiert, wenn der Spieler von weiter weg auf Kisten springt
  Problem ist, dass der Spieler einen Capsule Collider hat und mit diesem auf die Kiste drauf rutschen kann
  Box Collider als Lösung würde sehr viele andere Probleme ergeben (nicht von Kisten runter rutschen wenn man nur wenig drauf steht, kleine Slopes im Level können nicht überwunden werden)
* 0 - Eventuell das Element, dass Zombies in den Abgrund stürzen bzw. dumm sind, ausbauen
* 1 - ~~Waffen auf 1 2 3 legen~~
* 1 - ~~Granate sollte man vergrößern, ruhig 3-4 mal so groß und langsam bewegen lassen~~
* ~~1 @ Michi - Canvas scaler für GUI~~
* ~~1 @ Michi - Auf einer Kiste stehen und Flammenwerfer macht einem selbst schaden (Collider von kleinem Flammenwerfer anpassen)~~
* 1 - ~~UI beim Spieler sollte es geben.~~ 
* 1- ~~Kisten UI ist zu arg im Vordergrund.~~
* 1 - ~~Zombie sollte schreien, wenn er einen Spieler sieht~~
* 0 - ~~Ende ist komisch, falls man nicht stirbt~~
  0 - Irgend ne Endcondition wäre gut
  0 - ~~Vielleicht doch auf das sterben verzichten~~
  1 - ~~Bevor die Zombies spawnen, kommt ein Text mit "You won"!!!!~~

## Feedback Freunde

* Jump zu niedrig
* Jump zu horizontalem Movement Verhältnis passt nicht!

## Internes Feedback

Screenshot referenz: https://screens.famboot.de/michi/2022-07-19_20-59-34.png

### Iteration 1
* 1 - ~~Man kriegt noch Dmg Sound wenn man tot ist~~
      ~~Du kannst da einfach bei dem PlaySoundAtPoint ne if alive davor machen~~
      ~~Wenn man stirbt, und viele Zombies um einen rum sind. Dann laggts irgendwie extrem. Als ich eben auf denen drauf stand hats nicht so gelaggt~~
* 1 - ~~Das Sliden über den Boden sollten wir lösen~~
* 1 - ~~Man sollte nicht über Zombies springen können mMn. Sonst kann man das ganze Spiel durch Springen schaffen~~
* 3 - Vielleicht macht man am Ende, dass wenn ein Zombie einen trifft, dass er dann auch stirbt
* 3 - Kann man den Collider vom Zombie evtl. nur ganz wenig hoch machen? Sodass man von oben im Zombie "stehen" kann, iihn aber dennoch wegpusht bzw. er nicht in einen reinlaufen kann
* 1 - ~~Die Granate bei der Assault Rifle sieht man noch nicht so gut -> Projektil langsamer~~
* n - Die Explosion vom GL hat dem Zombie gerade einfach Kilometer weit weg gescheppert
* 3 - Das Feuer bei einem brennender Zombie ist glaub ich von der Position her nicht an den Zombie gebunden, oder?
* 3 - ~~Ammo vom Flamethrower sollte man entweder reduzieren, oder so viele Ammo-Pickups rein machen, dass man auch bis zum Maximum kommen kann~~
* 1 - ~~Melee Hitbox zu groß, ich hab grad 3 Zombies auf einen Schlag gekillt~~
* 1 - ~~YOU STILL DIED könnten wir ein ? noch hinmachen~~
* 3 - Vielleicht brauchen wir generell einen check, ob der Zombie zu lange versucht auf der gleichen Position zu laufen
* 2 - Wie heißt denn das Spiel? Dann könnte man noch ne Überschrift in so blutigem Stil oder so ins Hauptmenu packen
* 3 - Zombies sollten Kisten evtl. auch zerstören können
      Man kann mit Kisten voll cheesen
* 1 - ~~Level is laggy wenn groß~~
      ~~auch laggt das spiel wenn man mehr zombies reinmacht~~
* 1 - ~~Im Hauptmenu und Menu sollten die keybinds stehen~~
* 1 - ~~Die keybinds sollte man noch machen (Mausrad Waffe wechseln bspw.)~~

### Iteration 1 - Level
* 1 - ~~Die Plattformen sollten ein bisschen näher zueinander~~
      https://docs.unity3d.com/ScriptReference/Transform-hasChanged.html
      Alternativ ein Editor Script machen (MonoBehaviour mit MyBox \[ButtonMethod\])
* 1 - ~~Im Menu blocken sich die 2 Zombies im Käfig~~
* 1 - ~~Der Zombie nach dem ersten Haus beim Feuer ist ein bisschen arg im Vordergrund~~
* 2 - ~~Mehr Kisten als Obstacles wären gut~~
* 1 - ~~Bei der 3. Plattform sind 5 Zombies bei einander. War bei mir gerade jedes mal so~~
* 3 - Beim letzten Haus fehlt noch Feuerwerk ~~und irgendiwe läuft man da gegen die Türe~~
* 1 - ~~Der Zombie nach dem ersten Haus läuft immer gegen das Fass~~
* 2 - ~~Ich würde die Zombies am Ende all links spawnen lassen, und dann mit dem ewig großen Collider zum Spieler rennen. Dann kommen die schön der Reihe nach da rein~~
      ~~Vielleicht auch deren Walk Speed reduzieren~~
      ~~Und man sollte die Zombies langsamer Spawnen~~
* 1 - ~~Zombies werden außerhalb der Box gespawned, wenn das Spiel endet~~

### Iteration 2
* ~~Also die Kisten, die einem die Sicht blocken sind viiiiel zu viele~~
  ~~Man checkt garnicht durch~~
  ~~Mit ner Granate haben wir uns gerade einen Stein in den Weg geballert, sofern der nicht eh schon dort ist~~
* ~~Explosion vom Fass ist zu leise~~ (oder zu dumpf)
* ~~Die Hitbox vom Flammenwerfer ist zu breit. Man steckt manchmal "off-path" (also nicht auf unserer z beschränkten Ebene) Sachen in Brandt und stirbt dann, obwohl man an dem Objekt vor sich nicht dran war~~
* Evtl. macht ein Feuerwerk noch Sinn?
* ~~Nach dem ersten Haus, die Zombies klemmen wieder komplett~~
* Die Zombies relativ nah an der Laufebene irritieren noch immer
* ~~Den Nebel überm letzten Haus find ich komisch~~
* ~~Zombie Rotation nicht nur links/rechts~~
