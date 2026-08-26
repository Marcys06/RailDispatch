# RailDispatch — Gameplay

## 1. Charakter gry

RailDispatch jest sandboxowym symulatorem zarządzania ruchem kolejowym.

Główną rolą użytkownika jest projektowanie sieci kolejowej, planowanie usług, tworzenie rozkładów jazdy oraz nadzorowanie ruchu pociągów.

Gra nie posiada klasycznego systemu kampanii, misji ani warunku zwycięstwa.

Głównym celem rozgrywki jest sprawne funkcjonowanie zaprojektowanej sieci kolejowej.

## 2. Główna pętla rozgrywki

Podstawowa sesja składa się z następujących etapów:

1. Utworzenie mapy.
2. Przygotowanie terenu.
3. Budowa infrastruktury kolejowej.
4. Utworzenie stacji.
5. Utworzenie infrastruktury sterowania ruchem.
6. Utworzenie usługi kolejowej.
7. Zdefiniowanie pełnej trasy usługi.
8. Utworzenie rozkładu jazdy.
9. Przydzielenie taboru.
10. Uruchomienie usługi.
11. Automatyczne prowadzenie pociągu.
12. Nadzorowanie ruchu.
13. Reagowanie na problemy.
14. Analizowanie statystyk.

## 3. Budowa sieci

Użytkownik buduje sieć kolejową ręcznie.

Podstawową jednostką budowy jest pojedyncza kratka mapy.

Użytkownik może:

- układać tory,
- usuwać tory,
- tworzyć rozgałęzienia,
- tworzyć rozjazdy,
- tworzyć stacje,
- dodawać semafory,
- tworzyć połączenia pomiędzy fragmentami sieci.

System automatycznie analizuje utworzoną infrastrukturę.

System automatycznie wyznacza sekcje torowe na podstawie infrastruktury sterowania ruchem.

## 4. Tory

Sieć może posiadać zarówno pojedynczy tor, jak i dwa niezależne tory.

Pojedynczy tor umożliwia ruch w obu kierunkach.

Dwa niezależne tory umożliwiają prowadzenie ruchu w przeciwnych kierunkach oraz mijanie i wyprzedzanie pociągów.

Kierunek ruchu pociągu jest określany przez jego trasę oraz aktualną sytuację na sieci.

## 5. Stacje

Stacja jest punktem infrastruktury, w którym pociąg może realizować postój wynikający z rozkładu.

Pociąg zatrzymuje się na stacji zgodnie z parametrami usługi.

Postój jest elementem rozkładu jazdy.

Czas postoju może być określony dla konkretnego przystanku.

System pasażerski może generować pasażerów oczekujących na obsługę przez pociąg.

## 6. Usługi

Usługa jest logicznym opisem połączenia kolejowego.

Usługa jest tworzona przed przypisaniem do niej konkretnego taboru.

Usługa określa między innymi:

- nazwę,
- trasę,
- przystanki,
- kolejność punktów,
- rozkład,
- priorytet,
- wymagania dotyczące postoju.

Usługa nie jest bezpośrednio związana z jednym konkretnym pociągiem.

Taki podział umożliwia późniejsze przypisywanie różnych składów do tej samej usługi.

## 7. Trasa

Użytkownik określa pełną trasę usługi.

Trasa nie jest jedynie listą stacji.

Trasa zawiera przebieg przez sieć kolejową oraz wymagane punkty przejazdu.

System wykorzystuje trasę do automatycznego prowadzenia pociągu.

System może wyznaczać wymagane przebiegi przez rozjazdy.

Użytkownik zachowuje kontrolę nad wyborem całej trasy.

## 8. Rozkład jazdy

Rozkład jazdy określa planowany przebieg usługi w czasie.

Rozkład może określać:

- godzinę rozpoczęcia,
- kolejność przystanków,
- planowany czas przyjazdu,
- planowany czas odjazdu,
- czas postoju,
- częstotliwość,
- ograniczenia usługi.

Rozkład jest podstawowym mechanizmem planowania ruchu.

System porównuje rzeczywiste wykonanie przejazdu z planem.

## 9. Priorytet

Każda usługa posiada parametr priorytetu.

Priorytet jest początkowo prostym parametrem używanym przez system rozwiązywania konfliktów.

Zaawansowane reguły priorytetów mogą zostać dodane w późniejszych wersjach.

## 10. Pociągi

Pociąg jest fizycznym składem znajdującym się na mapie.

Pociąg składa się z niezależnych obiektów taborowych.

Podstawowymi elementami są:

- lokomotywy,
- wagony.

Wagon bez lokomotywy nie może samodzielnie poruszać się po sieci.

Pociąg może zmieniać skład podczas rozgrywki.

## 11. Sprzęganie i rozprzęganie

System umożliwia sprzęganie wagonów i lokomotyw.

System umożliwia rozprzęganie wagonów i lokomotyw.

Każdy wagon jest osobnym obiektem.

Każda lokomotywa jest osobnym obiektem.

Zmiana składu nie wymaga utworzenia nowego pociągu jako pojedynczego nierozdzielnego obiektu.

Dzięki temu możliwe jest odwzorowanie rzeczywistych operacji manewrowych.

## 12. Zmiana kierunku

Pociąg może zmienić kierunek jazdy.

Zmiana kierunku może wymagać wykonania manewru.

Lokomotywa może cofnąć skład na minimalną wymaganą odległość.

Cofanie może być wykorzystane do wykonania operacji manewrowych.

W sytuacji wymagającej przeprowadzenia lokomotywy na drugi koniec składu lokomotywa może wykonać manewr obiegania składu, jeżeli infrastruktura na to pozwala.

## 13. Automatyczne prowadzenie

Pociąg po uruchomieniu usługi jest prowadzony automatycznie.

System odpowiada za:

- realizację trasy,
- kontrolę prędkości,
- reakcję na sygnały,
- zatrzymywanie przed przeszkodami,
- obsługę rozjazdów,
- realizację postojów,
- wykonywanie wymaganych manewrów.

Użytkownik nie steruje bezpośrednio prędkością każdego pociągu.

Użytkownik pełni przede wszystkim funkcję dyspozytora.

## 14. Semafory

Semafory określają możliwość kontynuowania jazdy.

Stan semafora zależy od aktualnej sytuacji infrastruktury.

Semafor może nakazywać:

- jazdę,
- ograniczenie,
- zatrzymanie.

Szczegółowy model sygnalizacji zostanie opisany w `RAILWAY.md`.

## 15. Rozjazdy

Rozjazdy umożliwiają zmianę toru.

System automatycznie steruje rozjazdami zgodnie z ustalonym przebiegiem.

Interlocking zapobiega ustawieniu konfliktowych przebiegów.

Pociąg nie może przejechać przez rozjazd ustawiony w sposób niezgodny z jego trasą.

## 16. Konflikty ruchowe

System wykrywa konflikty pomiędzy pociągami.

Przykładowe konflikty obejmują:

- zajęcie sekcji przez inny pociąg,
- konflikt przebiegów,
- niedostępny rozjazd,
- semafor wskazujący zatrzymanie,
- przeszkodę na trasie,
- nieprawidłową trasę.

System próbuje automatycznie rozwiązywać konflikty zgodnie z zasadami ruchu i priorytetami.

Pociąg może oczekiwać na zwolnienie infrastruktury.

## 17. Błędy

System może wykrywać sytuacje niezgodne z zasadami symulacji.

Wykolejenie powoduje zatrzymanie pociągu.

Wykolejenie generuje zgłoszenie błędu.

Pozostałe błędy mogą powodować zatrzymanie pociągu i oczekiwanie na rozwiązanie problemu.

## 18. Pasażerowie

System pasażerski jest obecny już w wersji beta.

Pasażerowie są generowani w zależności od zapotrzebowania.

Liczba pasażerów może zmieniać się okresowo.

Początkowy interwał aktualizacji wynosi 60 minut czasu symulacji.

Pasażerowie mogą oczekiwać na stacjach.

Pasażerowie mogą korzystać z usług kolejowych.

System pasażerski pozostaje uproszczony w pierwszej wersji.

## 19. Towary

System towarowy nie jest podstawowym elementem wersji beta.

Architektura danych powinna jednak od początku umożliwiać obsługę ładunków.

Implementacja pasażerów powinna być przygotowana w sposób umożliwiający późniejsze wykorzystanie podobnego mechanizmu dla towarów.

## 20. Tabor

Tabor posiada parametry techniczne.

Podstawowe parametry obejmują:

- masę,
- długość,
- prędkość maksymalną,
- przyspieszenie,
- hamowanie,
- typ napędu,
- system zasilania.

System obsługuje:

- lokomotywy elektryczne,
- lokomotywy spalinowe,
- pojazdy hybrydowe,
- wagony.

System trakcji elektrycznej rozróżnia zasilanie DC i AC.

Parametry taboru są przechowywane w modelu umożliwiającym późniejsze rozszerzenie.

## 21. Długość składu

Długość pociągu jest obliczana na podstawie długości wszystkich jego elementów.

Wzór podstawowy:

`Długość pociągu = suma długości lokomotyw + suma długości wagonów`

Długość składu wpływa na między innymi:

- zajętość sekcji,
- możliwość zatrzymania na stacji,
- wymagania dotyczące manewrowania,
- pozycję składu na torze.

## 22. Czas symulacji

Symulacja działa w tickach o długości 20 ms czasu rzeczywistego.

Domyślne przyspieszenie wynosi:

`1 sekunda rzeczywista = 1 minuta czasu symulacji`

Użytkownik może zmienić prędkość symulacji w dowolnym momencie.

System symulacji powinien być niezależny od warstwy prezentacji.

## 23. Statystyki

Gra gromadzi podstawowe statystyki funkcjonowania sieci.

Statystyki mogą obejmować:

- liczbę uruchomionych pociągów,
- liczbę wykonanych kursów,
- punktualność,
- opóźnienia,
- liczbę pasażerów,
- wykorzystanie infrastruktury,
- liczbę zatrzymań,
- liczbę błędów,
- liczbę wykolejeń.

Statystyki nie stanowią obecnie podstawowego systemu ekonomicznego.

## 24. Brak klasycznego celu

Gra nie posiada wymogu osiągnięcia konkretnego celu.

Użytkownik może rozwijać sieć według własnego uznania.

Użytkownik może budować małe lokalne układy albo rozbudowane sieci kolejowe.

System powinien wspierać zarówno krótkie eksperymenty, jak i długotrwałe symulacje.

## 25. Zasada projektowa

Każdy system gameplayowy powinien być projektowany tak, aby:

- działał niezależnie od UI,
- był możliwy do testowania,
- posiadał jasno określone dane wejściowe i wyjściowe,
- umożliwiał późniejszą rozbudowę,
- nie wymagał silnika gier,
- wspierał deterministyczną symulację tam, gdzie jest to możliwe.

