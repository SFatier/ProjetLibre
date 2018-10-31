# ProjetLibre

MEMO PROJET API

supprimer le dossier Migrations dans le projet
creer votre base de donnée sql express
connecter cette base de donnee à visual studio dans explorateur de serveurs
Copier la chaine de connexion dans les propriétés de la base de donnée qui vient d'etre connecté
et la coller a la place de la mienne dans startup.cs
dans le dossier du projet ouvrir cmd et faire comme ligne de commande dotnet ef migrations add init
puis dotnet ef database update

MEMO PROJET APP

1- Creation du projet
2- Ajouter Identity -> selectionner all -> et le db context
3- Ajouter une classe dans data et supprimer le dossier Migrations
4- Modifier IdentityUser par le nom de la classe précédemment ajouté
5- ctrl + H => IdentityUser <=> ApplicationUser (nom de ma classe)
6- Retourner ds la class modifier l'heritage modifier dans le remplement
7- Modifier ApplicationUser par IdentityUser  dans les view _ManageNav et _LoginPartial
8- Creer la db dans la console du gestionnaire de package taper
	 => add-migration init
	 => update-database

(pour supprimer => Supprimer le dossier Migrations et taper drop-database ds la console => recreer la db)


Connection de la db dans VS

Cliquer sur l'explorateur de données 
Ajouter db en MSQL SERVER
AJouter comme non du serveur (Localdb)\MSSQL...
Authentification Win
AJouter le nom de la db 
Connexion


ps:  db son créé pour le moment 1 pour le projet API et 1 pour le projet app. Le but est d'utiliser que le projet API comme liaison de db
