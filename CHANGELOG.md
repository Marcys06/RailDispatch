# Changelog

## [Unreleased]

### Railway / Track Building

- Dodano model geometrii torów: TrackGeometry.
- Dodano obsługę kierunków zakrętów: CurveDirection.
- Dodano tryby budowania torów: TrackBuildMode.
- Dodano TrackBuilder odpowiedzialny za budowanie i usuwanie pojedynczych elementów toru.
- Dodano tory proste poziome i pionowe.
- Dodano cztery orientacje zakrętów: NorthEast, EastSouth, SouthWest, WestNorth.
- Dodano automatyczne logiczne łączenie nowego toru z istniejącymi sąsiadami.
- Dodano możliwość usuwania pojedynczego elementu toru prawym przyciskiem myszy.
- Zmieniono budowanie torów z przeciągania na stawianie pojedynczych elementów kliknięciem.
- Dodano wybór typu toru z poziomu sterowania: 1 = tor prosty, 2 = zakręt.
- Dodano zmianę orientacji toru prostego: H = poziomy, V = pionowy.
- Dodano obracanie zakrętu klawiszem R.
- Dodano panel narzędzia na mapie pokazujący aktualny typ i orientację toru.
- Poprawiono renderowanie zakrętów tak, aby ich końce były zgodne z punktami połączeń sąsiednich pól.
- Poprawiono geometrię krzywych Béziera dla czterech kierunków zakrętu.
- Naprawiono konflikty pomiędzy TrackType i TrackGeometry.
- Przywrócono poprawną strukturę MapControl po wcześniejszych zmianach.
- Dodano obsługę TrackBuilder w MapControl.
- Zweryfikowano kompilację całego rozwiązania.

### Build

- RailDispatch.Domain — build OK.
- RailDispatch.Simulation — build OK.
- RailDispatch.Infrastructure — build OK.
- RailDispatch.UI — build OK.
- RailDispatch.Tests — build OK.
- RailDispatch.App — build OK.

### Controls

- 1 — wybór toru prostego.
- 2 — wybór zakrętu.
- H — orientacja pozioma.
- V — orientacja pionowa.
- R — obrót zakrętu.
- LPM — postaw jeden element toru.
- PPM — usuń jeden element toru.
- MMB — przesuwanie kamery.
- Kółko myszy — zoom mapy.
