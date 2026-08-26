# RailDispatch — Data Model

## 1. Cel dokumentu

Dokument definiuje model danych gry RailDispatch.

Model danych opisuje obiekty świata gry, ich właściwości, relacje oraz zasady przechowywania danych.

Model został zaprojektowany z myślą o:
- symulacji ruchu kolejowego,
- planowaniu ruchu,
- budowie infrastruktury,
- tworzeniu usług i rozkładów,
- obsłudze pasażerów,
- późniejszym dodaniu przewozu towarów,
- zapisie i odczycie stanu gry,
- dalszej rozbudowie bez konieczności przebudowy podstawowych struktur.

---

## 2. Zasady modelu

RailDispatch rozdziela dane infrastruktury, taboru, planowania oraz aktualnego stanu symulacji.

Obiekt świata powinien posiadać stabilny identyfikator.

Identyfikatory powinny umożliwiać jednoznaczne odwoływanie się do obiektów podczas symulacji oraz zapisu gry.

Dane konfiguracyjne powinny być oddzielone od danych chwilowego stanu symulacji.

Parametry, które obecnie są stałe dla wszystkich obiektów, powinny być modelowane jako parametry możliwe do późniejszej indywidualizacji.

Model nie powinien zakładać, że pociąg jest pojedynczym obiektem fizycznym.

---

# 3. Główne grupy danych

Model danych obejmuje następujące grupy:

```text
Game
├── Map
│   ├── Terrain
│   ├── Track
│   ├── Junction
│   ├── Station
│   └── Signal
│
├── Rolling Stock
│   ├── Locomotive
│   ├── PassengerCar
│   ├── FreightCar
│   └── Train
│
├── Operations
│   ├── Route
│   ├── Service
│   ├── Timetable
│   ├── Stop
│   └── Priority
│
├── Passengers
│   ├── PassengerDemand
│   └── PassengerFlow
│
└── Simulation
    ├── SimulationState
    ├── SimulationTime
    └── TrainState
````

---

# 4. Game

`Game` reprezentuje cały stan rozgrywki.

### Przykładowe dane

```text
Game
├── Id
├── Name
├── Version
├── Map
├── Infrastructure
├── RollingStock
├── Routes
├── Services
├── Timetables
├── PassengerSystem
└── SimulationState
```

`Game` powinien umożliwiać zapis kompletnego stanu rozgrywki.

---

# 5. Map

Mapa reprezentuje przestrzeń świata gry.

Maksymalny rozmiar mapy wynosi:

```text
16384 × 16384
```

Jedna kratka mapy reprezentuje jedną jednostkę przestrzeni.

Mapa może być mniejsza od maksymalnego rozmiaru.

Mapa jest początkowo pusta.

Mapa zawiera teren.

Mapa nie zawiera wody.

---

# 6. Terrain

`Terrain` reprezentuje teren znajdujący się pod infrastrukturą.

Model terenu powinien pozwalać na późniejsze rozszerzenie parametrów.

Minimalny model może zawierać:

```text
TerrainCell
├── X
├── Y
└── Height
```

Wysokość terenu jest parametrem umożliwiającym późniejsze uwzględnienie nachyleń.

---

# 7. Track

`Track` reprezentuje fizyczny odcinek toru.

Tor jest budowany kratka po kratce.

### Podstawowe dane

```text
Track
├── Id
├── Start
├── End
├── Direction
├── Electrification
├── VoltageType
├── MaximumSpeed
├── Length
├── SectionId
└── JunctionConnections
```

`Length` jest wyliczana na podstawie geometrii toru.

`MaximumSpeed` określa maksymalną dozwoloną prędkość na danym odcinku.

---

# 8. Track direction

Tor może być:

```text
OneWay
TwoWay
```

Tor dwukierunkowy pozwala na jazdę w obu kierunkach.

Tor jednokierunkowy ogranicza dozwolony kierunek ruchu.

Kierunek aktualnego ruchu pociągu jest stanem symulacji, a nie właściwością samego toru.

---

# 9. Electrification

Infrastruktura kolejowa obsługuje elektryfikację.

Minimalny model:

```text
ElectrificationType
├── None
├── DC
└── AC
```

Dokładne napięcia mogą zostać dodane w przyszłości.

Typ elektryfikacji powinien być niezależny od typu pojazdu.

---

# 10. Junction

`Junction` reprezentuje rozjazd lub połączenie torów.

Rozjazd może posiadać wiele możliwych ustawień.

```text
Junction
├── Id
├── ConnectedTracks
├── CurrentPosition
└── AvailablePositions
```

Aktualna pozycja rozjazdu jest stanem infrastruktury.

Rozjazd może być sterowany przez system interlockingu.

---

# 11. Station

`Station` reprezentuje stację kolejową.

Stacja może posiadać wiele torów oraz peronów.

```text
Station
├── Id
├── Name
├── Position
├── Platforms
└── StationTracks
```

Stacja jest punktem infrastruktury oraz elementem planowania rozkładu jazdy.

Pociąg zatrzymujący się na stacji wykonuje postój wynikający z rozkładu.

---

# 12. Platform

`Platform` reprezentuje peron lub miejsce obsługi pasażerów.

```text
Platform
├── Id
├── StationId
├── TrackId
├── Length
└── Capacity
```

Połączenie peronu z torem pozwala określić miejsce zatrzymania pociągu.

Model powinien umożliwiać późniejszą obsługę różnych długości peronów.

---

# 13. Signal

`Signal` reprezentuje semafor.

Semafory mogą ograniczać możliwość wjazdu pociągu na określony odcinek.

```text
Signal
├── Id
├── Position
├── Direction
├── SignalType
├── SectionId
└── State
```

Stan semafora jest dynamiczny.

Semafor może zostać wykorzystany przez interlocking do zabezpieczenia trasy.

---

# 14. Automatic sections

Sekcje blokowe są wyznaczane automatycznie na podstawie infrastruktury i ustawionych semaforów.

Użytkownik nie musi ręcznie definiować każdego odcinka sekcji.

```text
BlockSection
├── Id
├── Tracks
├── EntrySignals
├── ExitSignals
└── Occupancy
```

`Occupancy` określa, czy sekcja jest zajęta.

---

# 15. Interlocking

`Interlocking` odpowiada za bezpieczne ustawianie przebiegów.

System interlockingu powinien kontrolować:

* zajętość sekcji,
* położenie rozjazdów,
* konflikty przebiegów,
* stan semaforów,
* możliwość ustawienia trasy.

Interlocking nie powinien samodzielnie wybierać całej trasy pociągu.

Użytkownik wybiera całą trasę przejazdu.

System następnie wykorzystuje interlocking do bezpiecznej realizacji wybranej trasy.

---

# 16. Route

`Route` reprezentuje zaplanowaną trasę przejazdu.

Trasa jest wybierana przez użytkownika.

```text
Route
├── Id
├── Name
├── Start
├── Destination
├── Waypoints
├── TrackPath
└── Direction
```

`TrackPath` zawiera kolejność odcinków toru.

Trasa może zawierać rozjazdy.

Trasa może przebiegać przez wiele stacji.

---

# 17. Train

`Train` reprezentuje aktualny skład kolejowy.

Pociąg nie powinien przechowywać wszystkich informacji o pojazdach jako jednego obiektu fizycznego.

Pociąg składa się z osobnych jednostek taboru.

```text
Train
├── Id
├── Name
├── Vehicles
├── Route
├── Service
├── Timetable
├── Priority
└── State
```

---

# 18. Vehicle

`Vehicle` jest bazowym pojęciem dla pojedynczego pojazdu.

Każdy wagon jest osobnym obiektem.

Każda lokomotywa jest osobnym obiektem.

```text
Vehicle
├── Id
├── Type
├── Length
├── Mass
├── MaximumSpeed
├── Acceleration
├── Braking
└── Power
```

---

# 19. Locomotive

`Locomotive` reprezentuje lokomotywę.

Lokomotywa może być:

```text
Electric
Diesel
Hybrid
```

Lokomotywa elektryczna posiada wymagany typ zasilania.

```text
Locomotive
├── PowerType
├── ElectrificationCompatibility
├── Power
├── TractionForce
├── MaximumSpeed
├── Acceleration
└── Braking
```

---

# 20. PassengerCar

`PassengerCar` reprezentuje wagon pasażerski.

```text
PassengerCar
├── Capacity
├── Occupancy
├── Length
├── Mass
└── PassengerType
```

Pojemność wagonu określa maksymalną liczbę pasażerów.

Obciążenie wagonu wpływa na masę całego składu.

---

# 21. FreightCar

`FreightCar` reprezentuje wagon towarowy.

Wersja beta może zawierać jedynie podstawową implementację.

```text
FreightCar
├── Capacity
├── CargoType
├── CargoAmount
├── Length
└── Mass
```

System towarowy powinien istnieć w modelu już od początku, nawet jeżeli pełna mechanika zostanie dodana później.

---

# 22. Train composition

Kolejność pojazdów w pociągu jest istotna.

```text
Train
    ↓
Vehicle[0] — Locomotive
Vehicle[1] — PassengerCar
Vehicle[2] — PassengerCar
Vehicle[3] — PassengerCar
```

Długość pociągu jest wyliczana:

```text
TrainLength =
    sum(Vehicle.Length)
```

Masa pociągu jest wyliczana:

```text
TrainMass =
    sum(Vehicle.Mass)
```

---

# 23. Coupling

Pojazdy mogą być łączone.

Połączenie powinno być reprezentowane niezależnie od obiektu `Train`.

```text
Coupling
├── VehicleA
├── VehicleB
└── State
```

Takie rozwiązanie pozwala obsłużyć rozdzielanie i łączenie składów.

---

# 24. Splitting trains

Pociąg może zostać rozdzielony na dwa lub więcej składów.

Przykład:

```text
Train A

Lokomotywa
Wagon 1
Wagon 2
Wagon 3
Wagon 4
```

Po rozdzieleniu:

```text
Train A
Lokomotywa
Wagon 1
Wagon 2

Train B
Wagon 3
Wagon 4
```

Wagon bez lokomotywy nie może samodzielnie się poruszać.

---

# 25. Joining trains

Dwa składy mogą zostać połączone.

System powinien zachować kolejność pojazdów.

Przykład:

```text
Train A
Lokomotywa + Wagony

+

Train B
Lokomotywa + Wagony
```

Po połączeniu powstaje jeden skład.

---

# 26. Reversing

Pociąg może zmieniać kierunek jazdy.

Zmiana kierunku nie musi oznaczać zmiany kolejności fizycznej pojazdów.

Kierunek jazdy jest stanem symulacji.

---

# 27. Shunting

System powinien umożliwiać manewry.

Lokomotywa może cofać skład na minimalną wymaganą odległość.

Brak możliwości wykonania manewru cofania może wymagać objechania składu przez lokomotywę.

Mechanika manewrowa powinna być uproszczona w pierwszej wersji, ale model danych powinien umożliwiać jej rozwój.

---

# 28. Train parameters

Minimalny model fizyczny pociągu obejmuje:

```text
MaximumSpeed
Acceleration
Braking
Mass
Length
```

Parametry mogą być indywidualne dla pojazdów.

Parametry składu są wyliczane na podstawie jego pojazdów.

---

# 29. TrainState

`TrainState` przechowuje dynamiczny stan pociągu.

```text
TrainState
├── Position
├── Direction
├── Speed
├── Acceleration
├── CurrentTrack
├── TrackOffset
├── CurrentRoutePosition
├── CurrentSignal
├── CurrentSection
├── IsStopped
├── IsDerailled
└── Error
```

Stan ten zmienia się podczas symulacji.

---

# 30. Derailment

Wykolejenie jest zdarzeniem symulacyjnym.

```text
IsDerailled = true
```

Po wykolejeniu pociąg zatrzymuje się.

System zgłasza błąd.

Szczegółowa mechanika skutków wykolejenia może zostać rozszerzona w przyszłości.

---

# 31. Service

`Service` reprezentuje usługę kolejową.

Usługa jest tworzona przed przypisaniem do niej konkretnego składu.

```text
Service
├── Id
├── Name
├── Route
├── Stops
├── Priority
├── TimetableTemplate
└── AssignedTrain
```

Usługa opisuje zamiar wykonania określonego połączenia.

---

# 32. Service priority

Usługa posiada tag priorytetu.

Minimalny model:

```text
Priority
```

Szczegółowy system priorytetów może zostać rozszerzony w przyszłości.

---

# 33. Timetable

`Timetable` reprezentuje rozkład jazdy przypisany do usługi.

```text
Timetable
├── Id
├── ServiceId
├── Stops
├── DepartureRules
└── ArrivalRules
```

Rozkład może być oparty o wzorzec.

---

# 34. Timetable template

System posiada wzór rozkładu, który można modyfikować.

```text
TimetableTemplate
├── Id
├── Name
├── Stops
├── DwellTimes
└── TimingRules
```

Wzorzec może zostać wykorzystany do tworzenia wielu podobnych rozkładów.

---

# 35. Stop

`Stop` reprezentuje postój pociągu.

```text
Stop
├── StationId
├── PlatformId
├── ArrivalTime
├── DepartureTime
└── DwellTime
```

Pociąg musi wykonać wymagany postój na stacji.

Czasy postoju są elementem rozkładu.

---

# 36. Route assignment

Użytkownik wybiera całą trasę.

Usługa otrzymuje wcześniej utworzoną trasę.

Skład jest następnie przypisywany do usługi.

```text
Route
    ↓
Service
    ↓
Train
    ↓
Timetable
```

---

# 37. Passenger system

System pasażerski jest częścią modelu gry już w wersji beta.

Pasażerowie są generowani i aktualizowani w interwałach czasowych.

Początkowy interwał wynosi:

```text
60 minut czasu symulacji
```

---

# 38. PassengerDemand

`PassengerDemand` opisuje zapotrzebowanie na przejazdy.

```text
PassengerDemand
├── OriginStation
├── DestinationStation
├── PassengerCount
└── GeneratedAt
```

Zapotrzebowanie może być generowane proceduralnie.

---

# 39. PassengerFlow

`PassengerFlow` reprezentuje przemieszczanie się pasażerów pomiędzy stacjami.

System powinien umożliwiać późniejsze dodanie bardziej zaawansowanych modeli wyboru trasy.

---

# 40. Cargo

System towarowy nie jest podstawowym elementem wersji beta.

Model danych powinien jednak od początku obsługiwać:

```text
CargoType
CargoAmount
CargoCapacity
Origin
Destination
```

Pełna mechanika przewozów towarowych może zostać dodana później bez przebudowy podstawowego modelu taboru.

---

# 41. Simulation

`SimulationState` reprezentuje stan działania symulacji.

```text
SimulationState
├── CurrentTime
├── Tick
├── SpeedMultiplier
├── IsRunning
├── Trains
├── Signals
├── Junctions
└── Events
```

---

# 42. Simulation time

Symulacja działa w stałym kroku:

```text
20 ms
```

Jeden rzeczywisty tick odpowiada:

```text
1 sekunda czasu rzeczywistego = 1 minuta czasu symulacji
```

Oznacza to przyspieszenie symulacji:

```text
60×
```

---

# 43. Simulation tick

Podczas każdego ticka system aktualizuje między innymi:

```text
Train movement
Train speed
Braking
Signals
Junctions
Interlocking
Station stops
Passenger state
Schedule state
Events
```

Stały tick pozwala na deterministyczniejsze działanie symulacji.

---

# 44. Event model

System powinien wykorzystywać zdarzenia dla istotnych zmian stanu.

Przykładowe zdarzenia:

```text
TrainArrivedAtStation
TrainDepartedStation
TrainStopped
TrainStarted
TrainJoined
TrainSplit
SignalChanged
JunctionChanged
RouteSet
TrainDerailled
ScheduleViolation
```

Model zdarzeń powinien być rozszerzalny.

---

# 45. Errors

Błędy symulacji powinny być reprezentowane jako dane.

```text
SimulationError
├── Id
├── Type
├── TrainId
├── Time
├── Description
└── Severity
```

Błąd nie powinien automatycznie kończyć całej symulacji.

Pociąg, którego dotyczy błąd, może zostać zatrzymany zależnie od typu błędu.

---

# 46. Save model

Stan gry powinien być możliwy do serializacji.

Minimalny zapis powinien obejmować:

```text
Game
Map
Infrastructure
RollingStock
Trains
Routes
Services
Timetables
PassengerState
SimulationState
```

Dane tymczasowe, które można bezpiecznie odtworzyć, nie muszą być przechowywane w identycznej postaci.

---

# 47. IDs

Każdy trwały obiekt powinien posiadać unikalny identyfikator.

Identyfikator powinien być niezależny od nazwy obiektu.

Zmiana nazwy stacji nie może zmienić jej `Id`.

---

# 48. Names

Nazwy są elementem prezentacyjnym i identyfikacyjnym dla użytkownika.

Przykładowe nazwy:

```text
Warszawa Centralna
407 Chopin
Linia IC 101
Tor 1
Semafor S12
```

Nazwy nie powinny być używane jako klucze relacji pomiędzy obiektami.

---

# 49. References

Relacje pomiędzy obiektami powinny być oparte na identyfikatorach.

Przykład:

```text
Service.RouteId
Service.AssignedTrainId
Stop.StationId
Stop.PlatformId
Train.ServiceId
Train.RouteId
Vehicle.TrainId
```

Takie rozwiązanie ułatwia serializację oraz późniejsze zarządzanie dużą liczbą obiektów.

---

# 50. Calculated values

Niektóre wartości powinny być wyliczane zamiast przechowywane jako niezależne dane.

Przykłady:

```text
TrainLength
TrainMass
TrainMaximumSpeed
PassengerOccupancy
RouteLength
```

Wyliczane wartości nie powinny powodować niespójności pomiędzy obiektami.

---

# 51. Extensibility

Model danych powinien umożliwiać przyszłe dodanie:

* nowych typów lokomotyw,
* nowych typów wagonów,
* wagonów towarowych,
* różnych typów ładunków,
* bardziej szczegółowej elektryfikacji,
* bardziej zaawansowanych parametrów fizycznych,
* bardziej zaawansowanego systemu pasażerskiego,
* różnych typów sygnalizacji,
* bardziej szczegółowych rozjazdów,
* większej liczby typów usług,
* bardziej zaawansowanych priorytetów,
* zdarzeń i statystyk eksploatacyjnych.

Rozszerzenia nie powinny wymagać zmiany podstawowego modelu `Train`, `Vehicle`, `Track`, `Route` i `Service`.

---

# 52. Beta scope

Wersja beta powinna zawierać co najmniej:

```text
Map
Terrain
Track
Junction
Station
Platform
Signal
BlockSection
Interlocking

Locomotive
PassengerCar
Train
Coupling

Route
Service
Timetable
Stop
Priority

PassengerDemand
PassengerFlow

SimulationState
TrainState
SimulationError
```

Model towarowy powinien istnieć częściowo, ale pełna mechanika przewozu towarów nie jest wymagana w pierwszej becie.

---

# 53. Core relationship diagram

```text
                         ┌──────────────┐
                         │     Map      │
                         └──────┬───────┘
                                │
                ┌───────────────┼────────────────┐
                │               │                │
                ▼               ▼                ▼
             Track          Station          Signal
                │               │                │
                ▼               ▼                ▼
            Junction         Platform       BlockSection
                │                                │
                └──────────────┬─────────────────┘
                               ▼
                         Interlocking
                               │
                               ▼
                            Route
                               │
                               ▼
                           Service
                               │
                    ┌──────────┴──────────┐
                    ▼                     ▼
               Timetable                Train
                                          │
                                          ▼
                                       Vehicle
                                          │
                         ┌────────────────┼────────────────┐
                         ▼                ▼                ▼
                    Locomotive      PassengerCar      FreightCar
```

---

# 54. Fundamental rule

Najważniejszą zasadą modelu jest rozdzielenie:

```text
Infrastructure
    ≠
Rolling Stock
    ≠
Operations
    ≠
Simulation State
```

Infrastruktura opisuje świat.

Tabor opisuje dostępne pojazdy.

Operacje opisują sposób wykorzystania infrastruktury i taboru.

Symulacja opisuje aktualny stan świata.

Takie rozdzielenie stanowi podstawę dalszego rozwoju RailDispatch.
