# RailDispatch — Vision

## 1. Cel projektu

RailDispatch jest sandboxowym symulatorem zarządzania ruchem kolejowym, którego głównym celem jest planowanie i prowadzenie ruchu pociągów na własnoręcznie zaprojektowanej sieci kolejowej.

Gra koncentruje się na pracy dyspozytora oraz tworzeniu i realizacji rozkładów jazdy.

## 2. Główne założenia

- Gra działa wyłącznie w 2D.
- Cała rozgrywka odbywa się na jednej mapie.
- Mapa jest siatką o maksymalnym rozmiarze 16384 × 16384 pól.
- Jedno pole odpowiada jednej jednostce przestrzeni symulacji.
- Mapa może być mniejsza niż 16384 × 16384.
- Mapa nie zawiera wody.
- Teren może być generowany proceduralnie.
- Tory są budowane ręcznie przez użytkownika, kratka po kratce.
- Sieć kolejowa może zawierać jeden lub dwa niezależne tory.
- Pociągi mogą poruszać się w obu kierunkach.
- Dwa niezależne tory umożliwiają mijanie i wyprzedzanie pociągów.
- Użytkownik projektuje pełną trasę pociągu.
- Trasa może być następnie realizowana automatycznie.
- Rozjazdy i semafory są elementami infrastruktury sterującymi ruchem.
- System interlocking zapobiega konfliktowym przebiegom.
- Sekcje torowe są wyznaczane automatycznie na podstawie infrastruktury i konfiguracji użytkownika.
- Pociąg jest złożony z niezależnych obiektów reprezentujących lokomotywy i wagony.
- Wagony mogą być sprzęgane i rozprzęgane.
- Wagon bez lokomotywy nie może samodzielnie poruszać się po sieci.
- Lokomotywa może zmieniać skład poprzez sprzęganie i rozprzęganie wagonów.
- Skład może wykonywać manewry wymagające cofania.
- Długość pociągu jest sumą długości lokomotyw i wagonów.
- Pociągi posiadają uproszczone parametry techniczne umożliwiające realistyczne zachowanie.
- Podstawowe parametry obejmują między innymi prędkość maksymalną, przyspieszenie, hamowanie i masę.
- System trakcji rozróżnia napęd elektryczny, spalinowy i hybrydowy.
- System elektryczny rozróżnia zasilanie DC i AC.
- Pociągi mogą wykoleić się w przypadku naruszenia zasad ruchu lub ograniczeń symulacji.
- Wykolejenie powoduje zatrzymanie pociągu i zgłoszenie błędu.
- Stacje wymagają postoju pociągu zgodnie z rozkładem.
- Pasażerowie są obecni już w wersji beta.
- System towarowy zostanie przygotowany architektonicznie od początku, ale pełna obsługa różnych typów towarów zostanie dodana później.
- Liczba pasażerów może zmieniać się okresowo; początkowy interwał wynosi 60 minut czasu symulacji.
- Symulacja wykorzystuje tick 20 ms.
- Czas symulacji płynie w przyspieszeniu 1 minuta symulacji na 1 sekundę czasu rzeczywistego.
- Prędkość symulacji może być zmieniana przez użytkownika w dowolnym momencie.
- Rozgrywka ma charakter sandboxowy i symulacyjny.
- Gra nie posiada klasycznego celu końcowego.
- Gra koncentruje się na statystykach i poprawnym funkcjonowaniu sieci.
- Lokomotywy i tabor są oparte na rzeczywistych odpowiednikach.
- System parametrów taboru jest przygotowany do dalszej rozbudowy.
- Priorytet pociągu istnieje od początku jako podstawowy parametr.
- Zaawansowany system priorytetów może zostać rozbudowany w późniejszych wersjach.

## 3. Główna pętla rozgrywki

Podstawowa pętla rozgrywki wygląda następująco:

1. Użytkownik buduje infrastrukturę kolejową.
2. Użytkownik tworzy stacje i pozostałe elementy sieci.
3. Użytkownik tworzy usługę kolejową.
4. Użytkownik definiuje pełną trasę usługi.
5. Użytkownik przypisuje tabor do usługi.
6. Użytkownik tworzy rozkład jazdy.
7. System automatycznie prowadzi pociąg zgodnie z trasą i zasadami ruchu.
8. Użytkownik obserwuje sytuację na mapie.
9. Użytkownik reaguje na konflikty, opóźnienia, awarie i inne zdarzenia.
10. System generuje statystyki działania sieci.

## 4. Rola użytkownika

Użytkownik pełni przede wszystkim funkcję dyspozytora i projektanta ruchu kolejowego.

Użytkownik odpowiada za:

- projektowanie infrastruktury,
- wyznaczanie tras,
- tworzenie usług,
- tworzenie rozkładów jazdy,
- przypisywanie taboru,
- zarządzanie ruchem,
- reagowanie na problemy,
- optymalizowanie przepustowości sieci.

## 5. Realizm

Realizm techniczny jest jednym z głównych założeń projektu.

Realizm nie oznacza pełnej symulacji fizycznej.

Model fizyczny jest uproszczony, ale jego architektura umożliwia późniejsze zwiększanie dokładności.

## 6. UI

Interfejs użytkownika jest minimalistyczny.

Mapa stanowi główny i praktycznie jedyny obszar rozgrywki.

Elementy interfejsu mają wspierać:

- budowę infrastruktury,
- zarządzanie pociągami,
- tworzenie usług,
- tworzenie rozkładów,
- obserwowanie ruchu,
- kontrolowanie infrastruktury.

## 7. Technologia

Projekt jest tworzony bez silnika gier.

Projekt musi być możliwy do rozwijania w Visual Studio.

Kod źródłowy jest przechowywany w publicznym repozytorium GitHub.

Projekt wykorzystuje kontrolę wersji Git oraz workflow oparty o branche, issues, milestones, pull requesty, releases i changelog.

## 8. Zakres wersji beta

Wersja beta ma zawierać działający prototyp całego podstawowego przepływu:

- mapa,
- budowa torów,
- rozjazdy,
- semafory,
- sekcje,
- interlocking,
- stacje,
- pociągi,
- lokomotywy,
- wagony,
- sprzęganie,
- rozprzęganie,
- trasy,
- usługi,
- rozkłady,
- automatyczne prowadzenie pociągów,
- pasażerowie,
- podstawowe statystyki,
- wykolejenia i obsługę błędów.

## 9. Poza zakresem początkowym

Następujące elementy nie są wymagane w pierwszej wersji:

- pełna symulacja fizyczna,
- rozbudowana ekonomia,
- zaawansowana gospodarka towarowa,
- multiplayer,
- kampania,
- fabuła,
- klasyczne cele strategiczne,
- system zwycięstwa i przegranej,
- zaawansowana grafika 2D,
- silnik gry.

## 10. Zasada rozwoju

Architektura projektu powinna od początku umożliwiać rozbudowę systemów bez konieczności przebudowy całej aplikacji.

Systemy wymagane dopiero w przyszłości powinny posiadać odpowiednie punkty rozszerzeń już w pierwszej implementacji.

