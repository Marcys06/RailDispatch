# TRAIN_SYSTEM

## Cel

System pociągów odwzorowuje rzeczywiste składy jako zestawy niezależnych pojazdów.

## Pojazdy

Lokomotywa jest osobnym obiektem.

Wagon jest osobnym obiektem.

Każdy wagon może być sprzęgnięty z innymi pojazdami.

Wagon bez lokomotywy nie może samodzielnie się poruszać.

## Skład

Pociąg posiada uporządkowaną listę pojazdów.

Kolejność pojazdów ma znaczenie.

Długość pociągu jest sumą długości wszystkich jego pojazdów.

## Sprzęganie

Użytkownik może łączyć wagony i lokomotywy w dowolnym dozwolonym układzie.

Sprzęganie jest wykonywane jako operacja manewrowa.

## Rozdzielanie

Użytkownik może rozdzielić istniejący skład.

Po rozdzieleniu powstają niezależne grupy pojazdów.

Każdy pojazd zachowuje własną tożsamość.

## Lokomotywy

Obsługiwane typy lokomotyw:

- elektryczne DC
- elektryczne AC
- spalinowe
- hybrydowe HZT

## Parametry

Każdy pojazd posiada model parametrów technicznych.

Model obejmuje co najmniej:

- Vmax
- przyspieszenie
- hamowanie
- masę
- długość

Model pozostaje rozszerzalny.

## Manewry

Lokomotywa może cofać skład na określoną minimalną odległość.

W sytuacji wymagającej zmiany strony składu lokomotywa może wykonać oblot, jeżeli infrastruktura na to pozwala.

## Wykolejenie

Nieprawidłowa jazda może doprowadzić do wykolejenia.

Wykolejony pociąg zatrzymuje się.

System zgłasza błąd wymagający reakcji użytkownika.
