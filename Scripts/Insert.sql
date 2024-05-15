DROP PROCEDURE IF EXISTS [PS_InsertChambre]; 
go


CREATE PROCEDURE[PS_InsertChambre]
            --Add the parameters for the stored procedure here avec le nom des parametres envoyés ^pour les@ nom bd sans les @
          
         @Id_Chambre int, @Etage int, @Prix decimal(5,2), @Etat Bit, @Code_TypeChambre varchar(2),@Code_Localisation varchar(2) , @Memo varchar(25) 
         --@Id_Chambre int output
           AS
           BEGIN

              
            INSERT INTO dbo.CHAMBRE (Id_Chambre, Etage ,  Prix , Etat, Code_TypeChambre, Code_Localisation, Memo) VALUES (@Id_Chambre, @Etage ,  @Prix , @Etat, @Code_TypeChambre, @Code_Localisation, @Memo)

               --set @Id_Chambre = SCOPE_IDENTITY()
           END
           Go