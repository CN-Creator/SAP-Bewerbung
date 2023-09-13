# Bewerbungsprojekte SAP

In diesem Repository befinden sich 4 Projekte, welche ich selbst entworfen habe und für diese Bewerbung teilen kann.

## Projekte
 - Gebäudeautomation (/gebaudeautomation)
 - Internetprogrammierung (/internetprog)
 - TODO-Projekt
 - Umwelt-Detektive (/umwelt-detective-master) 

### Gebäudeautomation
Ziel des Projektes war es im Raum A1-11 in der Halle 21 (Lok 21) in der Nähe des Hochschul-Campus durch eine gebäude-telematische Lösung ein gesundes und angenehmes Klima sicherzustellen, da der von der TH Wildau gemietete Raum in Zukunft sowohl als aktiv als Arbeitsraum als auch als Vorzeigeraum für Telematikprojekte der Bereiche Robotik und Gebäudeautomation dienen soll.   Der Begriff “Klima” ist hierbei sehr breit gefasst und kann sowohl eine gute Luftqualität als auch eine optimale Arbeitsatmosphäre bedeuten.

Zu meiner Aufgabe gehörte die Umsetzung des Backends. Das Backend mit seinen verschiedenen Funktionen läuft auf einem Raspberry PI 4b. Hier werden die Sensordaten der verschiedenen Sensoren, welche über den ESP-32 an den Raspberry PI weitergeleitet werden, in einer Datenbank gespeichert, um die Daten dem Nutzer auch in Zukunft bereitstellen zu können. Darüber hinaus ist im Backend die Funktionalität zur Steuerung des LED-Streifens realisiert.

### Internetprogrammierung
Aufgabe dieses Projektes war die Implementierung von 2 APIs in einem von uns frei gewähltem Projekt. Hieraus entstand für mich die Idee ein Higher-Lower Game mittels Spielkarten zu programmieren.

### TODO-Projekt
Die Idee des TODO-Projektes entstand aus der von Ihnen erhaltenen E-Mail, welche dieses als mögliches Beispiel aufgeführt hat.

### Umwelt-Detektive
Dieses Projekt ist eigentlich eine Gruppenarbeit von mir mit 2 weiteren Kommilitonen. Ziel des Projektes war es Grundschülern das Thema Nachhaltigkeit mithilfe des NAO Roboters näher zu bringen. Hierfür durften wir uns ein Thema frei überlegen und umsetzen. Da wir zu Beginn mehrere Ansätze für das Erkennen unsere Gegenstände hatten und sich der von mir eingeschlagene Weg als der beste Weg herausgestellt hat, habe ich die weitere Programmierung des Programmes übernommen und meine Kommilitonen haben sich auf die Dokumentation, das Erstellen der physischen Karten sowie das Erarbeiten der Informationen konzentriert.

2 Bedienungsanleitung

Die folgenden Schritte erläutern die notwendigen Interaktionen mit dem NAO
während des Spiels und den generellen Ablauf.

2.1 NAO hochfahren

Der NAO bewegt sich während des gesamten Spiels nicht, das heißt, der Roboter
muss in eine bequeme, feste Sitzposition gebracht werden. Danach wird der NAO
durch kurzes Drücken des Knopfs auf der Brust angemacht.

2.2 Spiel starten

Das Spiel wird mit dem Betätigen des rechten Fußbumpers aus Sicht des NAOs
gestartet. Im Anschluss folgt eine Begrüßung vom NAO-Roboter mitsamt Einführung
und Erklärung der nächsten Schritte. Es kann direkt zur Themenauswahl
gesprungen werden, indem der vordere Kopfsensor angetippt wird.

2.3 Spielregeln anhören

Die Spielregeln werden mit dem Betätigen des hinteren Kopfsensors vorgelesen.
Auch hier wird nochmal erklärt, dass das Spiel durch Berühren des vorderen
Kopfsensors gestartet wird.

2.4 Thema auswählen

Nachdem das Spiel gestartet wurde, liest der Roboter die hinterlegten Modulnamen
vor und der Spieler kann sich im Anschluss eines der Module aussuchen. Die
Modulauswahl passiert so, dass zwischen den Modulen mit dem rechten und linken
Fußbumpern gewechselt wird. Das aktuell ausgewählte Modul wird jedes Mal
vorgelesen. Durch Berühren der vorderen Kopfsensors wird das Spiel mit dem
aktuellen Modul gestartet.

2.5 Objektkarte scannen

Eine Objektkarte wird gescannt, indem der QR-Code auf der Rückseite der Karte
zum Kopf des NAOs hingehalten wird. Am besten funktioniert das Scannen durch
das Stecken der Karte in die Forschungsstation. Sobald das Objekt erkannt wurde,
gibt der NAO ein Signal mit dem erkannten Objekt und sagt, ob das richtige Objekt
gewählt wurde. Dieser Schritt wird je nach Anzahl der Objekte des ausgewählten
Moduls entsprechend oft wiederholt.

2.6 Auswertung und Neustart

Nachdem das letzte Objekt des Moduls eingescannt und die entsprechenden
Informationen vorgelesen wurden, gibt der Roboter die gebrauchten Züge aus und
erzählt nochmal Tipps zum jeweiligen Modul. Im Anschluss kann wie im Kapitel “2.2
Spiel starten” das Spiel wiederholt werden.

# Application Projects SAP
In this repository, there are 4 projects which I have designed myself and can share for this application.

## Projects
- Building Automation (/buildingautomation)
- Internet Programming (/internetprog)
- TODO Project
- Environmental Detectives (/environmental-detective-master)

### Building Automation
The aim of the project was to ensure a healthy and pleasant climate in Room A1-11 in Hall 21 (Location 21), near the university campus, through a building telematics solution. The room rented by TH Wildau is intended to serve in the future both as an active workspace and as a showcase for telematics projects in the fields of robotics and building automation. The term "climate" is broadly defined here and can mean both good air quality and an optimal working atmosphere.

My task included the implementation of the backend. The backend, with its various functions, runs on a Raspberry Pi 4b. Sensor data from various sensors, which are forwarded to the Raspberry Pi via the ESP-32, are stored in a database so that the data can be made available to the user in the future. Furthermore, functionality for controlling the LED strip is implemented in the backend.

### Internet Programming
The task of this project was to implement 2 APIs in a project of our free choice. From this emerged my idea to program a Higher-Lower Game using playing cards.

### TODO Project
The idea for the TODO project came from an email I received from you, which listed this as a possible example.

### Environmental Detectives
This project is actually group work that I did with two other fellow students. The goal of the project was to introduce elementary school students to the topic of sustainability with the help of the NAO robot. We were free to come up with and implement a topic. Since we initially had multiple approaches for recognizing our objects and the path I took turned out to be the best, I took over further programming of the program, while my colleagues focused on documentation, creating physical cards, and compiling information.

User Manual
The following steps explain the necessary interactions with the NAO during the game and the general procedure.

2.1 Start Up NAO
The NAO does not move throughout the entire game, meaning the robot must be placed in a comfortable, fixed sitting position. Afterward, the NAO is turned on by briefly pressing the button on its chest.

2.2 Start the Game
The game is started by pressing the right foot bumper from the NAO's perspective. This is followed by a greeting from the NAO robot along with an introduction and explanation of the next steps. You can directly jump to theme selection by tapping the front head sensor.

2.3 Listen to Game Rules
The game rules are read out by pressing the rear head sensor. Here, it is again explained that the game is started by touching the front head sensor.

2.4 Choose a Theme
After the game has started, the robot reads the stored module names and the player can then choose one of the modules. Module selection occurs by toggling between modules using the right and left foot bumpers. The currently selected module is read out each time. The game starts with the currently selected module by touching the front head sensor.

2.5 Scan Object Card
An object card is scanned by holding the QR code on the back of the card to the NAO's head. Scanning works best by inserting the card into the research station. As soon as the object is recognized, the NAO emits a signal with the recognized object and states whether the correct object has been chosen. This step is repeated as many times as there are objects in the selected module.

2.6 Evaluation and Restart
After the last object of the module has been scanned and the corresponding information has been read out, the robot displays the number of moves used and provides additional tips for the respective module. The game can then be restarted as described in the section "2.2 Start the Game".