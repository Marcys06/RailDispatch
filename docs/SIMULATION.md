# SIMULATION

## Cel

Symulacja odpowiada za czas, ruch pociągów, infrastrukturę oraz zdarzenia.

## Tick

Podstawowy tick symulacji wynosi 20 ms.

Logika symulacji korzysta ze stałego kroku czasowego.

Renderowanie nie jest bezpośrednio związane z tickiem.

## Skala czasu

Jedna minuta czasu symulacji odpowiada jednej sekundzie czasu rzeczywistego.

Skala podstawowa wynosi 60:1.

Użytkownik może zmieniać tempo symulacji w dowolnym momencie.

## Ruch

System automatycznie wylicza ruch pociągów.

Na ruch wpływają:

- masa
- Vmax
- przyspieszenie
- hamowanie
- długość składu
- ograniczenia infrastruktury
- stan semaforów
- dostępność trasy

## Zatrzymanie

Pociąg zatrzymuje się przed ograniczeniem uniemożliwiającym dalszą jazdę.

## Wykolejenie

Nieprawidłowe warunki ruchu mogą spowodować wykolejenie.

Wykolejony pociąg zostaje zatrzymany.

System zgłasza błąd.

## Determinizm

Symulacja powinna być możliwie deterministyczna.

Wynik powinien zależeć od stanu początkowego oraz wykonanych poleceń, a nie od liczby klatek renderowania.
