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