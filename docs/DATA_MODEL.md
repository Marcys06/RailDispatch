# DATA_MODEL

## Cel

Model danych reprezentuje świat gry oraz jego stan.

## Map

Mapa posiada:

- Width
- Height
- Terrain
- RailwayInfrastructure

Maksymalny rozmiar wynosi 16384 × 16384.

## Track

Track reprezentuje odcinek toru.

Track posiada geometrię oraz połączenia z sąsiednimi elementami.

## Switch

Switch reprezentuje rozjazd.

Switch posiada aktualny stan oraz dostępne przebiegi.

## Signal

Signal reprezentuje semafor.

Signal kontroluje dostęp do chronionej sekcji.

## BlockSection

BlockSection reprezentuje automatycznie wyznaczoną sekcję torową.

Sekcja może być wolna albo zajęta.

## Station

Station reprezentuje stację.

Stacja posiada lokalizację oraz punkty obsługi pociągów.

## RollingStock

RollingStock reprezentuje pojedynczy pojazd.

Lokomotywa i wagon są specjalizacjami pojazdu.

## Train

Train reprezentuje fizyczny skład.

Train posiada listę RollingStock.

## Service

Service reprezentuje usługę przewozową.

Service posiada trasę, rozkład, priorytet oraz przypisany tabor.

## Route

Route reprezentuje kompletną trasę wybraną przez użytkownika.

## Schedule

Schedule reprezentuje plan czasowy usługi.

## PassengerDemand

PassengerDemand reprezentuje aktualne zapotrzebowanie pasażerskie.

## Extensibility

Model powinien umożliwiać dodanie towarów bez przebudowy podstawowego modelu Train.
