
````markdown
# RailDispatch — Data Model

## 1. Cel dokumentu

Dokument definiuje model danych gry RailDispatch.

Model danych opisuje:

- mapę,
- teren,
- infrastrukturę kolejową,
- tory,
- rozjazdy,
- sekcje torowe,
- semafory,
- stacje,
- perony,
- trasy,
- usługi,
- rozkłady jazdy,
- tabor,
- pociągi,
- pasażerów,
- zapotrzebowanie pasażerskie,
- stan symulacji.

Model powinien być niezależny od interfejsu użytkownika.

Model powinien umożliwiać późniejszą rozbudowę o przewozy towarowe bez przebudowy podstawowej architektury pociągów.

---

# 2. Zasady modelu

## 2.1. Identyfikatory

Każdy trwały obiekt świata gry powinien posiadać unikalny identyfikator.

Przykładowo:

```text
MapId
TrackId
SwitchId
SignalId
SectionId
StationId
PlatformId
RouteId
ServiceId
ScheduleId
TrainId
RollingStockId
PassengerDemandId
````

Identyfikatory nie powinny zależeć od pozycji obiektu na mapie.

---

## 2.2. Jednostki

Podstawową jednostką przestrzeni jest jedna kratka mapy.

```text
1 tile = 1 jednostka przestrzeni
```

Maksymalny rozmiar mapy:

```text
16384 × 16384
```

Mapa może być mniejsza.

Prędkość powinna być przechowywana w jednostce jednoznacznej dla silnika symulacji.

Masa powinna być przechowywana w kilogramach lub tonach, przy czym wybór jednostki musi być jednolity w całym projekcie.

Czas symulacji powinien być reprezentowany w sposób umożliwiający dokładne obliczenia.

---

# 3. Map

`Map` reprezentuje cały świat gry.

Mapa składa się z:

```text
Map
├── Terrain
├── RailwayInfrastructure
├── Stations
├── Signals
├── Trains
└── SimulationState
```

Podstawowe właściwości:

```text
Id
Width
Height
Terrain
Infrastructure
```

`Width` oraz `Height` określają rzeczywisty rozmiar mapy.

Maksymalna wartość obu parametrów wynosi:

```text
16384
```

---

# 4. Terrain

`Terrain` reprezentuje teren znajdujący się pod infrastrukturą.

Beta nie wymaga rozbudowanego systemu terenu.

Model powinien jednak przewidywać:

```text
TerrainType
Elevation
```

oraz możliwość późniejszego dodania:

```text
Slope
SurfaceType
```

Mapa beta nie posiada zbiorników wodnych.

---

# 5. RailwayInfrastructure

`RailwayInfrastructure` przechowuje elementy infrastruktury kolejowej.

Główne elementy:

```text
Tracks
Switches
Signals
Sections
Stations
Platforms
```

Infrastruktura jest częścią mapy.

Infrastruktura nie powinna przechowywać stanu pociągu.

---

# 6. Track

`Track` reprezentuje odcinek toru.

Tor może składać się z wielu kolejnych elementów przestrzennych.

Podstawowe dane:

```text
TrackId
Start
End
Length
Geometry
Direction
TrackType
SectionId
```

Tor może być:

* jednokierunkowy,
* dwukierunkowy.

Docelowo użytkownik może budować dwa niezależne tory równolegle.

Dwa równoległe tory pozostają niezależnymi elementami infrastruktury.

---

# 7. TrackGeometry

`TrackGeometry` opisuje fizyczny przebieg toru na mapie.

Geometria jest budowana kratka po kratce.

Przykładowo:

```text
(10,10)
(11,10)
(12,10)
(13,11)
(14,12)
```

Geometria powinna umożliwiać:

* obliczenie długości,
* określenie pozycji pociągu,
* określenie kierunku,
* wykrywanie zajęcia toru,
* wyznaczanie kolejnych elementów trasy.

---

# 8. Switch

`Switch` reprezentuje rozjazd.

Rozjazd posiada:

```text
SwitchId
Position
IncomingTrack
OutgoingTracks
CurrentPosition
Locked
```

Rozjazd może posiadać więcej niż jeden możliwy przebieg.

Rozjazd jest sterowany przez system interlockingu.

Rozjazd nie może zostać przestawiony, jeżeli jego stan jest zablokowany dla aktywnego przebiegu.

---

# 9. Signal

`Signal` reprezentuje semafor.

Semafor posiada:

```text
SignalId
Position
Direction
ProtectedSection
Aspect
```

Semafor może kontrolować dostęp do kolejnego odcinka lub przebiegu.

Semafory są elementami infrastruktury.

Semafory nie wyznaczają samodzielnie całej trasy pociągu.

---

# 10. BlockSection

`BlockSection` reprezentuje sekcję torową używaną przez system zabezpieczenia ruchu.

Sekcje są automatycznie wyznaczane na podstawie infrastruktury i rozmieszczenia semaforów.

Sekcja posiada:

```text
SectionId
Tracks
Occupied
Reserved
ReservedByTrainId
```

Stan sekcji może być:

```text
Free
Reserved
Occupied
```

System może rozszerzyć ten model o dodatkowe stany w przyszłości.

---

# 11. Interlocking

`Interlocking` nie jest pojedynczym obiektem infrastruktury, lecz systemem zarządzającym zależnościami pomiędzy:

```text
Signals
Switches
Sections
Routes
Trains
```

System interlockingu zapewnia, że konfliktowe przebiegi nie zostaną jednocześnie ustawione.

Interlocking powinien blokować:

* zajęte sekcje,
* zarezerwowane sekcje,
* konfliktowe rozjazdy,
* konfliktowe przebiegi.

---

# 12. Station

`Station` reprezentuje stację kolejową.

Stacja posiada:

```text
StationId
Name
Position
Platforms
```

Stacja może posiadać wiele peronów.

Stacja jest miejscem obsługi pasażerów.

Pociąg zatrzymujący się na stacji musi posiadać odpowiedni punkt postoju w swoim rozkładzie.

---

# 13. Platform

`Platform` reprezentuje peron lub miejsce obsługi pociągu.

Peron posiada:

```text
PlatformId
StationId
TrackId
Length
Position
```

Długość peronu jest niezależna od długości pociągu.

System może w przyszłości sprawdzać, czy długość pociągu umożliwia pełną obsługę przy danym peronie.

---

# 14. Route

`Route` reprezentuje kompletną trasę przejazdu wybraną przez użytkownika.

Użytkownik wybiera całą trasę.

Trasa może zawierać:

```text
Track
Switch
Section
Station
Platform
```

Przykładowa struktura:

```text
Route
  ↓
Track
  ↓
Switch
  ↓
Track
  ↓
Station
  ↓
Track
  ↓
Switch
  ↓
Track
```

Trasa jest logicznym przebiegiem przejazdu.

Trasa nie jest tym samym co automatycznie ustawiany przebieg semaforowy.

System może na podstawie trasy automatycznie wyznaczać wymagane przebiegi.

---

# 15. Service

`Service` reprezentuje usługę kolejową.

Usługa powinna istnieć przed przypisaniem do niej konkretnego taboru.

Usługa posiada:

```text
ServiceId
Name
RouteId
ScheduleId
Priority
AssignedTrainId
```

Priorytet jest obecnie podstawowym parametrem klasyfikacji ruchu.

System powinien umożliwiać późniejsze rozszerzenie priorytetu o bardziej rozbudowany system klas pociągów.

---

# 16. Schedule

`Schedule` reprezentuje rozkład jazdy.

Rozkład jest przypisany do usługi.

Rozkład powinien zawierać listę punktów:

```text
ScheduleStop
```

Każdy punkt może określać:

```text
StationId
PlatformId
ArrivalTime
DepartureTime
MinimumStopTime
```

Rozkład może posiadać różne czasy postoju dla różnych stacji.

Pociąg musi zatrzymać się na stacji zgodnie z wymaganiami rozkładu.

---

# 17. ScheduleStop

`ScheduleStop` reprezentuje pojedynczy postój pociągu.

Podstawowe dane:

```text
StationId
PlatformId
ArrivalTime
DepartureTime
StopDuration
```

Wartość czasu postoju może wynikać z różnicy pomiędzy czasem przyjazdu i odjazdu.

System powinien umożliwiać późniejsze dodanie:

```text
PassengerExchangeTime
OperationalStop
TechnicalStop
```

---

# 18. RollingStock

`RollingStock` reprezentuje pojedynczy pojazd kolejowy.

`RollingStock` jest abstrakcją wspólną dla:

```text
Locomotive
Carriage
```

Każdy pojazd posiada własny identyfikator.

Podstawowe parametry:

```text
RollingStockId
Length
Mass
MaxSpeed
Acceleration
Braking
```

Parametry powinny być dostępne w modelu nawet wtedy, gdy w wersji beta część wartości jest wspólna dla wielu pojazdów.

---

# 19. Locomotive

`Locomotive` jest specjalizacją `RollingStock`.

Lokomotywa posiada dodatkowe dane:

```text
Power
TractionType
VoltageSystem
```

Obsługiwane rodzaje trakcji:

```text
Electric
Diesel
Hybrid
```

System elektryczny powinien rozróżniać:

```text
DC
AC
```

Model powinien umożliwiać późniejsze dodanie konkretnych systemów zasilania.

---

# 20. Carriage

`Carriage` jest specjalizacją `RollingStock`.

Wagon posiada:

```text
CarriageType
PassengerCapacity
```

Model wagonu powinien być przygotowany na późniejsze dodanie wagonów towarowych.

---

# 21. Train

`Train` reprezentuje fizyczny skład znajdujący się w symulacji.

Pociąg posiada:

```text
TrainId
RollingStock
CurrentRoute
CurrentPosition
CurrentSpeed
CurrentDirection
CurrentSection
CurrentService
State
```

Pociąg składa się z pojedynczych obiektów `RollingStock`.

Przykład:

```text
Train
├── Locomotive
├── Carriage
├── Carriage
├── Carriage
└── Carriage
```

---

# 22. Train Length

Długość pociągu jest obliczana dynamicznie.

Wzór:

```text
TrainLength =
    LocomotiveLength
    + Sum(CarriageLength)
```

Każdy pojazd posiada własną długość.

Zmiana składu automatycznie zmienia długość pociągu.

---

# 23. Train Movement

Pojedynczy wagon nie może samodzielnie poruszać się jako pociąg.

Ruch całego składu jest kontrolowany przez `Train`.

Lokomotywa zapewnia siłę trakcyjną.

Wagony są elementami składu ciągniętymi lub pchanymi przez lokomotywę.

Model powinien jednak zachować każdy pojazd jako osobny obiekt.

---

# 24. Train Direction

Pociąg może poruszać się w obu kierunkach.

Kierunek powinien być określany względem geometrii trasy.

Zmiana kierunku może nastąpić:

* na stacji,
* na końcu trasy,
* podczas manewrów,
* po wykonaniu odpowiedniej operacji użytkownika.

---

# 25. Train Splitting

Skład może zostać rozdzielony.

Operacja rozdzielenia tworzy dwa niezależne składy.

Przykład:

```text
Train A

[Loco][A][B][C][D]
```

po rozdzieleniu:

```text
Train A
[Loco][A][B]

Train B
[C][D]
```

Pociąg bez lokomotywy nie może samodzielnie rozpocząć ruchu.

---

# 26. Train Joining

Dwa składy mogą zostać połączone.

Operacja połączenia tworzy jeden skład złożony z pojazdów obu składów.

Przykład:

```text
Train A
[Loco][A][B]

Train B
[C][D]
```

po połączeniu:

```text
Train A
[Loco][A][B][C][D]
```

System powinien sprawdzić możliwość fizycznego wykonania operacji.

---

# 27. Reversing

Pociąg może zmienić kierunek.

System powinien umożliwiać zmianę kierunku bez konieczności przebudowy obiektu `Train`.

Jeżeli wymagane jest przestawienie lokomotywy na drugi koniec składu, system powinien traktować to jako operację manewrową.

Pociąg może cofnąć się na ograniczoną odległość.

Jeżeli cofnięcie nie jest możliwe, lokomotywa może wymagać objechania składu.

---

# 28. Passenger

`Passenger` reprezentuje pojedynczego pasażera.

Model beta nie wymaga szczegółowej symulacji każdego pasażera.

System może przechowywać pasażerów jako agregaty zapotrzebowania.

---

# 29. PassengerDemand

`PassengerDemand` reprezentuje zapotrzebowanie pomiędzy punktami sieci.

Podstawowe dane:

```text
OriginStationId
DestinationStationId
PassengerCount
GenerationInterval
```

W becie liczba pasażerów może być aktualizowana co:

```text
60 minut czasu symulacji
```

Model powinien umożliwiać późniejsze dodanie:

```text
PassengerType
TransferPreference
DestinationWeight
TimePreference
```

---

# 30. Cargo

Model beta nie wymaga aktywnego systemu towarowego.

Model danych powinien jednak pozostawić możliwość dodania:

```text
Cargo
CargoType
CargoAmount
CargoDemand
CargoStation
```

System towarowy powinien korzystać z tej samej podstawowej infrastruktury pociągów.

---

# 31. Train State

Pociąg powinien posiadać stan operacyjny.

Przykładowe stany:

```text
Stopped
Accelerating
Running
Braking
Waiting
Arrived
Departing
Reversing
Shunting
Coupling
Decoupling
Error
Derailed
```

Lista stanów może zostać rozszerzona podczas implementacji.

---

# 32. Train Failure

Pociąg może wykoleić się w wyniku błędnego działania infrastruktury lub ruchu.

Po wykolejeniu:

```text
Train.State = Derailed
```

Pociąg przestaje kontynuować normalną jazdę.

System powinien zgłosić błąd użytkownikowi.

Szczegółowy model uszkodzeń może zostać dodany w przyszłości.

---

# 33. Relationships

Najważniejsze relacje modelu:

```text
Map
 ├── RailwayInfrastructure
 │    ├── Track
 │    ├── Switch
 │    ├── Signal
 │    └── BlockSection
 │
 └── Station
      └── Platform

Service
 ├── Route
 ├── Schedule
 └── Train

Train
 └── RollingStock
      ├── Locomotive
      └── Carriage

Station
 └── PassengerDemand
```

---

# 34. Ownership

Obiekt nadrzędny powinien odpowiadać za istnienie logicznie należących do niego elementów.

Przykładowo:

```text
Map
 └── Infrastructure

Station
 └── Platforms

Train
 └── RollingStock

Service
 ├── Route
 └── Schedule
```

Nie oznacza to koniecznie fizycznego zagnieżdżenia obiektów w kodzie.

Relacje mogą być realizowane poprzez identyfikatory.

---

# 35. References

Obiekty powinny używać identyfikatorów do odwoływania się do innych obiektów świata.

Przykład:

```text
Train.CurrentServiceId
Train.CurrentRouteId
Train.CurrentSectionId
```

Takie podejście ogranicza sprzężenie pomiędzy modelami.

---

# 36. Runtime State

Stan symulacji nie powinien być utożsamiany ze statyczną definicją obiektu.

Przykład:

```text
TrainDefinition
```

opisuje parametry pojazdu.

Natomiast:

```text
TrainState
```

opisuje jego aktualny stan w symulacji.

Podział powinien umożliwiać wielokrotne wykorzystanie tej samej definicji taboru.

---

# 37. Definitions vs Instances

System powinien rozróżniać definicje i instancje.

Przykład:

```text
LocomotiveDefinition
        ↓
Locomotive
```

oraz:

```text
CarriageDefinition
        ↓
Carriage
```

Definicja opisuje typ pojazdu.

Instancja reprezentuje konkretny pojazd znajdujący się w świecie gry.

---

# 38. Serialization

Model danych powinien być możliwy do zapisania i odtworzenia.

Docelowo zapis gry powinien obejmować:

```text
Map
Terrain
Infrastructure
Stations
Routes
Services
Schedules
RollingStock
Trains
PassengerDemand
SimulationTime
```

Stan chwilowy symulacji powinien być możliwy do odtworzenia z zapisu.

---

# 39. Versioning

Format danych zapisu powinien posiadać wersję.

Przykład:

```text
SaveVersion = 1
```

Zmiany modelu danych nie powinny powodować bezwarunkowej utraty istniejących zapisów.

System migracji zapisów może zostać dodany wraz z rozwojem projektu.

---

# 40. Extensibility

Model powinien być projektowany z myślą o przyszłej rozbudowie.

Planowane rozszerzenia:

```text
Cargo
FreightTrains
AdvancedPassengerSimulation
MultipleTractionSystems
DetailedRollingStock
Maintenance
Failures
AdvancedSignalling
AdvancedDispatching
```

Rozszerzenia nie powinny wymagać przebudowania podstawowych relacji:

```text
Map
Track
Route
Service
Train
RollingStock
Station
Schedule
```

---

# 41. Beta Scope

Wersja beta powinna implementować przede wszystkim:

* mapę,
* teren,
* tory,
* rozjazdy,
* sekcje,
* semafory,
* interlocking,
* stacje,
* perony,
* trasy,
* usługi,
* rozkłady,
* lokomotywy,
* wagony,
* pociągi,
* łączenie składów,
* rozdzielanie składów,
* ruch pociągów,
* podstawowe parametry fizyczne,
* pasażerów,
* podstawowe zapotrzebowanie pasażerskie.

System towarowy powinien być przygotowany architektonicznie, ale nie musi być aktywny w pierwszej becie.

---

# 42. Zasada nadrzędna

Model danych powinien odzwierciedlać rzeczywisty świat kolejowy w takim stopniu, w jakim jest to potrzebne do działania symulatora.

Model nie powinien komplikować implementacji bez wyraźnej wartości dla symulacji.

Każda abstrakcja powinna mieć uzasadnienie w mechanice gry, symulacji lub przyszłej rozbudowie projektu.
