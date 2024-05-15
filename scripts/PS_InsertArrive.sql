-- click droit sur la bd pour exécuter dans visual studio
-- si erreur actualiser le serveur, ouvrir le dossier proc stockée et supprimer et relancer
 DROP PROCEDURE IF EXISTS [dbo].[PS_InsertArrive]; 
 go
CREATE PROCEDURE[dbo].[PS_InsertArrive]
            --Add the parameters for the stored procedure here avec le nom des parametres envoyés ^pour les@ nom bd sans les @
          
         @Id_Reserve int, @Id_Client int, @Date_Arrive datetime, @Id_Chambre int, @Recu_Par varchar(2),
           @Id_Arrive int output
           AS
           BEGIN

              
            INSERT INTO dbo.ARRIVE (Id_Reserve, Id_Client, Date_Arrive, Id_Chambre, Recu_Par) VALUES (@Id_Reserve,  @Id_Client, @Date_Arrive, @Id_Chambre, @Recu_Par)

               set @Id_Arrive = SCOPE_IDENTITY()
           END
           Go