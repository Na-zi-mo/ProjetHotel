USE [bdhotel24]
GO
/****** Object:  StoredProcedure [dbo].[PS_InsertReservation]    Script Date: 2024-05-15 14:28:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE[dbo].[PS_InsertReservation]
            --Add the parameters for the stored procedure here avec le nom des parametres envoyés ^pour les@ nom bd sans les @
          
         --@Prenom varchar(15), @NomFamille varchar(15), @Age int,@Date_CreationBidon datetime,@Actif tinyint,@Code_Province varchar(2),@Id_Profession int,@Total_Ventes decimal,
           @Id_Client int, 
		   @Date_Reserve datetime , 
		   @Date_Debut datetime,
		   @Date_Fin datetime,
		   @Confirme bit,
		   @Id_Reserve int output
           AS
           BEGIN

              
            INSERT INTO dbo.RESERVE(Id_Client,Date_Reserve,Date_Debut,Date_Fin,Confirme) VALUES (@Id_Client, @Date_Reserve , @Date_Debut,@Date_Fin,@Confirme)

               set @Id_Reserve = SCOPE_IDENTITY()
           END