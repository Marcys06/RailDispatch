@'
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