## MA-24 - Projet scolaire de recherche en programmation
## Animation de sprites dans un programme C-sharp

## Problématique : animation de sprites dans une fenêtre à travers un jeu de plateforme

- [x] Afficher une partie de plusieurs images dans des picturebox.

- [ ] Faire l'animation du sprite à l'écran, au moyen d'un timer.

- [ ] Déplacer le sprite de 10 pixels dans la direction de la flèche pressée sur le clavier.

- [ ] Implémenter une gravité pour que le sprite tombe de la plate-forme

- [ ] Implémenter l'animation de saut lorsque l'utilisateur appuie sur la barre d'espace

- [ ] Définir des limites de bord pour éviter que le sprite sorte de l'écran.

- [ ] Implémenter l'animation d'attaque

- [ ] Implémenter l'ennemi et ses animations au moyen d'une boucle et d'un random

- [ ] Implémenter l'effet de poussée quand on frappe l'ennemi et quand on est touché

- [ ] Implémenter la fin du jeu lorsqu'un des deux joueurs est tombé de la plate-forme

- [ ] Tester le bon fonctionnement et la fluidité de l'animation.


## Tests unitaires :

- [ ] Contrôle que les sprites défilent dans le bon ordre

- [ ] Contrôle que toutes les touches font le bon déplacement

- [ ] Test si le sprite ne peut sortir de la fenêtre affichée

- [ ] Test de la fluidité de l'animation


## Références :

Source : https://codes-sources.commentcamarche.net/forum/affich-878141-comprend-pas-les-sprites 

« Il faut bien dessiner toutes les étapes de l'animation de ton personnage mais au lieu d'avoir plusieurs images on en charge une seule en mémoire qui contient toutes ses étapes dans un ordre précis. 
(Ce qu’on appelle alors, une « sprite-sheet »).
La couleur magenta ou lime ou n'importe quelle autre couleur unique (pas de compression jpeg pour les sprites), qui n'est pas utilisée par les sprites sera la couleur de fond, ce qui permet de ne pas l'afficher au moment du rendu.

Dans un vrai jeu tout se passe dans une boucle principale, on teste les entrées utilisateurs ( clavier, joystick .. ) mais on peut aussi coder ça avec les événements ou avec un timer. 

Au moment du rendu, il suffit d'afficher le sprite avec la méthode DrawImage qui prend en paramètre un rectangle source qui correspond à l'emplacement du sprite sur l'image et un rectangle destination qui correspond à l'endroit sur l'écran ou l'on va dessiner le sprite. 
En jouant avec ses 2 rectangles on peut varier les mouvements du personnage et sa position. »

### Références pour la solution :

La méthode DrawImage est issue de GDI+, qui est une librairie déjà intégrée à tous les projets.
Ceci évite de devoir recourir à un framework externe, tel que XNA ou DirectX. 

### Les trois étapes de base, commentées :
source : https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-gdi

### Ressources, libres de droit :

- [x] « tileset », pour jeu de plateforme : 
https://www.gameart2d.com/free-platformer-game-tileset.html 

- [x] « sprite » de chevalier :
https://www.gameart2d.com/the-knight-free-sprites.html
(« spritesheets » réalisées par moi-même).

- [x] « sprites » de guerrier sombre et chef suprême :
https://opengameart.org/content/dark-soldier-rework 
