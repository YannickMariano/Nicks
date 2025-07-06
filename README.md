# Nick-sProject

## Description

Ce repository contient une application desktop développée en **C# avec WPF**, utilisant une architecture **MVC**.  
Elle est compatible avec toutes les versions de **.NET à partir de .NET 6.0**, et s'appuie sur **PostgreSQL** comme base de données.

---

##  Technologies utilisées

- [.NET 6.0 ou supérieur](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [WPF (Windows Presentation Foundation)](https://learn.microsoft.com/en-us/dotnet/desktop/wpf/)
- [PostgreSQL](https://www.postgresql.org/)
- [Npgsql](https://www.nuget.org/packages/Npgsql) 

---

## Structure du projet
├── /Model/ 

├── /Controller/ 

├── /Service/
 
├── /View/
 
├── /Utils/ # Helpers, Mappers, autre fonctions

├── /Ressource/ # Images, styles, icônes, dictionnaires de ressources

├── /Test/ # Pour mettre les fichiers de test UI pour les backends



---

## Étapes à suivre après avoir cloné le projet

### 1. Cloner le projet

```bash
git clone https://github.com/votre-utilisateur/Nick_sProject.git
cd Nick_sProject

```
### 2 : Restaurer les dépendances NuGet
```bash
dotnet restore

```

---

## Collaboration Git (organisation des branches)

Après avoir cloné le projet, **chaque division** (Backend, Frontend) crée une branche principale dédiée à son domaine.  
Ensuite, chaque développeur crée sa propre branche à partir de celle de son équipe.

### Exemple de structure :

- `main` (branche principale de production)
- `backend` (branche principale des devs back)
  - `feature/backend-ajout-commande`
  - `bugfix/backend-connexion`
- `frontend` (branche principale des devs front)
  - `feature/frontend-affichage-commande`
  - `ui/frontend-refonte-style`

### Commande pour créer une branche personnelle à partir d'une branche d'équipe :

```bash
git checkout -b nouvelle_branche branche_existante

"# Nick-sProject" 
