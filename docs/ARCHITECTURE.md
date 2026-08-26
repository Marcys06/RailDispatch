

## 1. Cel dokumentu

Dokument opisuje architekturę techniczną gry RailDispatch.

Architektura ma umożliwiać rozwój projektu od prostego prototypu do pełnego symulatora planowania i prowadzenia ruchu kolejowego.

Projekt jest tworzony w Visual Studio z wykorzystaniem .NET i C#, bez zewnętrznego silnika gier.

---

## 2. Główne założenia

- Gra jest symulatorem ruchu kolejowego działającym w 2D.
- Głównym elementem rozgrywki jest planowanie i prowadzenie ruchu kolejowego.
- Użytkownik projektuje infrastrukturę kolejową.
- Użytkownik buduje trasy przejazdu.
- Użytkownik tworzy usługi kolejowe.
- Użytkownik przypisuje składy do usług.
- Użytkownik tworzy rozkłady jazdy.
- Użytkownik obserwuje działanie sieci.
- System automatycznie prowadzi pociągi zgodnie z ustalonymi zasadami.
- System sygnalizacji i interlockingu odpowiada za bezpieczeństwo ruchu.
- Pociągi mogą zmieniać kierunek jazdy.
- Składy mogą być rozdzielane i łączone.
- Symulacja działa w stałym kroku czasowym 20 ms.

---

## 3. Warstwy aplikacji

Architektura projektu powinna być podzielona na logiczne warstwy.

```text
+--------------------------------------------------+
|                    Presentation                  |
|              UI / Rendering / Input              |
+--------------------------------------------------+
|                    Application                  |
|       Commands / Services / Game Controllers     |
+--------------------------------------------------+
|                      Domain                     |
|   Trains / Tracks / Stations / Signals / Routes |
+--------------------------------------------------+
|                    Simulation                   |
|       Physics / Time / Movement / Scheduling     |
+--------------------------------------------------+
|                  Infrastructure                 |
|       Save / Load / Configuration / Storage      |
+--------------------------------------------------+