TH Wildau | INW | Telematik | Internetprogrammierung

---

Lösung von Aufgabenblatt B2
===========================

Erarbeitet von
--------------

- Niklas Schmidt

Kooperationen
-------------

- keine

Aufwandsabschätzung
-------------------

| Student         | Arbeitszeit in h |
| --------------- |:----------------:|
| Niklas Schmidt  | 20               |

Allgemeine Beschreibung der Lösung
----------------------------------

Die vorliegende Single-Page-Website stellt ein einfaches Kartenspiel gegen den Zufall dar.

Aus einem gemischten französischem Kartenblatt mit 52 Karten wird die erste Karte gezogen und in der Mitte angezeigt. 
Der Spieler gibt mit dem Klick auf einen der Button "Higher" oder "Lower" unter der aktuellen Karte eine Vorhersage ab, ob die nächste Karte im Deck einen höheren oder niedrigeren Wert hat.

Jede Karte besteht aus einem Bild und einer Farbe.
Da es in einem 52-Karten-Deck jede Karte nur einmal gibt, lassen sie sich leicht vergleichen. Die Karte mit dem kleinsten Wert ist die Karo 2, die Karte mit dem höchsten Wert ist das Kreuz Ass.

Für die Farben gilt: Karo < Herz < Pik < Kreuz.
Für die Bilder gilt: 2 < ... < 10 < Bube < Dame < König < Ass

Beim Vergleich wird zuerst das Bild geprüft und nur bei gleichem Bildwert wird auch die Farbe verglichen, um eine Entscheidung zu treffen.

Zusätzliche Funktionen
----------------------

- vorherige Karte wird am linken Bildschirmrand verkleinert angezeigt
- zu Beginn wird dort das Astronomy Picture Of the Day angezeigt, da es noch keine vorherige Karte gibt
- die Punkte werden gezählt und angezeigt, sobald der Spieler falsch liegt, geht die Punktzahl auf 0
- darunter ist der Highscore der aktuellen Session (bei Neuladen wird er gelöscht)
- Highscore aktualisiert sich, wenn Nutzer falsch tippt
- Bei richtiger Vorhersage wird dies durch grüne Nav-Bar und einen Sound symbolisiert
- Bei falscher Vorhersage wird dies durch rote Nav-Bar und einen Sound symbolisiert

Verwendete APIs
---------------

1. Deck of Cards API
- [Deck of Cards API - Doku](https://deckofcardsapi.com/)
- [Swagger Doku der verwendeten API-Funktionen](https://app.swaggerhub.com/apis/niklas.schmidt/deck_of_cards/1.0.0)
2. Random Facts API
- [Random Facts API - Doku](https://api-ninjas.com/api/facts)
- [Swagger Doku der verwendeten API-Funktionen](https://app.swaggerhub.com/apis/niklas.schmidt/deck_of_cards/1.0.0)
3. NASA APIs
- [NASA API - Doku](https://api.nasa.gov/index.html)
- [Swagger Doku der verwendeten API-Funktionen](https://app.swaggerhub.com/apis/niklas.schmidt/deck_of_cards/1.0.0)
