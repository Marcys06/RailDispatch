# ARCHITECTURE

## Cel

Architektura ma umożliwiać rozwój gry bez korzystania z zewnętrznego silnika.

## Warstwy

### Domain

Modele i reguły świata.

### Simulation

Logika czasu i zachowania obiektów.

### Infrastructure

Mapa, zapis danych oraz techniczne implementacje systemów.

### Application

Przypadki użycia i koordynacja systemów.

### UI

Renderowanie i obsługa wejścia użytkownika.

## Zależności

UI korzysta z Application.

Application korzysta z Domain oraz Simulation.

Simulation korzysta z Domain.

Domain nie zależy od UI.

## Testowalność

Logika symulacji powinna być testowalna bez uruchamiania UI.

Interlocking powinien posiadać testy jednostkowe.

Logika tras powinna posiadać testy jednostkowe.

## Rozszerzalność

Architektura powinna umożliwić późniejsze dodanie:

- towarów
- bardziej szczegółowej fizyki
- rozbudowanych parametrów trakcyjnych
- bardziej zaawansowanej gospodarki
- dodatkowych typów infrastruktury
