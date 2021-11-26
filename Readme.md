# MA-24 - Projet de recherche en programmation
## Animation de sprites dans un programme C

## Problématique : animation de sprites dans une fenêtre


- [ ] Afficher une partie de plusieurs images dans des picturebox.

Ceci est 

- [ ] Faire défiler le sprite suivant de l'animation à l'écran, au moyen d'un timer.

- [ ] Faire défiler le sprite suivant de l'animation à l'écran, au moyen des touches du clavier.

- [ ] Déplacer le sprite de 10 pixels dans la direction de la flèche pressée sur le clavier.

- [ ] Définir des limites de bord pour éviter que le sprite sorte de l'écran.

- [ ] Tester le bon fonctionnement et la fluidité de l'animation.


## Tests unitaires :

- [ ] Contrôle que les sprites défilent dans le bon ordre

- [ ] Contrôle que toutes les touches font le bon déplacement

- [ ] Test si le sprite ne peut sortir de la fenêtre affichée

- [ ] Test de la fluidité de l'animation


## Références :

Source : https://codes-sources.commentcamarche.net/forum/affich-878141-comprend-pas-les-sprites 

«… Je voudrais savoir si quelqu'un a déjà utilisé les sprites pour faire bouger un personnage par exemple ? 
(…) J'ai comme projet de faire un petit jeu en 2d tout simple »

### Réponse du forum :

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
source : https://docs.microsoft.com/en-us/dotnet/framework/winforms/advanced/how-to-render-images-with-		gdi

### Exemple d’application - comment dessiner une image dans une picture box :
![Dessiner une image sur un picturebox](https://raw.githubusercontent.com/laurentbarraud/AnimSprites/master/Drawing-an-image-on-a-picturebox-Stackoverflow.jpg)

source : https://stackoverflow.com/questions/24620464/how-to-draw-an-image-onto-a-picturebox-image

### Une des syntaxes possibles de la fonction DrawImage (surchargeable avec plus d’arguments) :
![Syntaxe de DrawImage](https://raw.githubusercontent.com/laurentbarraud/AnimSprites/master/Fonction-Drawimage-syntaxe-simple-MSDN.jpg)

source : https://docs.microsoft.com/en-us/dotnet/api/system.drawing.graphics.drawimage

### Ressources, libres de droit :

- [x] « tileset », pour jeu de plateforme : 
https://www.gameart2d.com/free-platformer-game-tileset.html 

- [x] « sprite » de chevalier :
https://www.gameart2d.com/the-knight-free-sprites.html
(« spritesheets » réalisées par moi-même).

- [x] « sprites » de guerrier sombre et chef suprême :
https://opengameart.org/content/dark-soldier-rework 
