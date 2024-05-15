USE bdhotel24
GO
drop table TRX
drop table DEPART
drop table ARRIVE
drop table TYPETRX

drop table DE
drop table RESERVE
drop table AYANT
drop table CHAMBRE
drop table LOCALISATION
drop table COMMODITE
drop table TYPECHAMBRE

drop table CLIENT
GO
Create Table TYPECHAMBRE
(
	Code_TypeChambre Varchar(2) Not Null,
	Description Varchar(25),
	Nbr_Dispo Int,
	PRIMARY KEY(Code_TypeChambre)
)

Create Table RESERVE
(
	Id_Reserve Int Identity(1,1) Not Null,
	Id_Client Int ,
	Date_Reserve Datetime,
	Date_Debut Datetime,
	Date_Fin Datetime,
	Confirme Bit,
	PRIMARY KEY(Id_Reserve)
)

Create Table DE
(   Id_De  int Identity(1,1) Not Null,

	Id_Reserve Int,
	Id_Chambre Int,

	Attribuee Bit,
	PRIMARY KEY(Id_De)
)

CREATE TABLE COMMODITE                       
(
	Code_Commodite varchar(2) not null,
	Description varchar(25),
	PRIMARY KEY(Code_Commodite) 
)
CREATE TABLE AYANT                    
(	Id_Ayant int Identity(1,1) Not Null,

Id_Chambre Int ,
	Code_Commodite varchar(2), 

	PRIMARY KEY(Id_Ayant)
)

CREATE TABLE CHAMBRE
(
  	Id_Chambre int  Not Null,

	Etage int,
	Prix decimal(5,2),
	Etat Bit,
	Code_TypeChambre varchar(2) ,
	Code_Localisation varchar(2) ,
	Memo varchar(25),
	PRIMARY KEY(Id_Chambre)
)


CREATE TABLE  CLIENT
(
  Id_Client  Int IDENTITY(1,1) not null,
  Nom varchar(25),
  Adresse varchar(25),
  Telephone varchar(10),
  Num_Carte varchar(15),
  Type_Carte varchar(15),
  Date_Exp datetime,
  Solde_Du decimal (12,2),
  PRIMARY KEY (Id_Client)
)

create table ARRIVE
(   Id_Arrive Int IDENTITY(1,1) not null,
	Id_Reserve Int ,
	Id_Client Int,
	Date_Arrive Datetime,
	Id_Chambre Int ,
	Recu_Par varchar(2),
	PRIMARY KEY(Id_Arrive)
)
create table DEPART
(   Id_Depart Int IDENTITY(1,1) not null,
	Id_Arrive Int,
	Date_Depart datetime,
	Conf_Par varchar(2),
	PRIMARY KEY(Id_Depart)
)
create table LOCALISATION
(
	Code_Localisation varchar(2),
	Description varchar(25),
	PRIMARY KEY(Code_Localisation)
)

Create table TRX
(
	Id_Trx int identity(1,1) not null,
	Id_Arrive int,
	Date_Trx datetime,
	Code_TypeTrx varchar(2),
	Montant decimal(10,2),
    Conf_Par varchar(2),
	Reportee bit,	 
	PRIMARY KEY(Id_Trx) 
)

Create Table TYPETRX
(
	Code_TypeTrx varchar(2) not null,
	Description varchar(25),
	Db_Cr  varchar(2),
	PRIMARY KEY(Code_TypeTrx) 
)





ALTER TABLE RESERVE
ADD FOREIGN KEY (Id_Client)
REFERENCES CLIENT(Id_Client)

ALTER TABLE DE
ADD FOREIGN KEY (Id_Reserve)
REFERENCES RESERVE(Id_Reserve)

ALTER TABLE DE
ADD FOREIGN KEY (Id_Chambre)
REFERENCES CHAMBRE(Id_Chambre)



ALTER TABLE CHAMBRE
ADD FOREIGN KEY (Code_TypeChambre)
REFERENCES TYPECHAMBRE(Code_TypeChambre)

ALTER TABLE CHAMBRE
ADD FOREIGN KEY (Code_Localisation)
REFERENCES LOCALISATION(Code_Localisation)

ALTER TABLE AYANT
ADD FOREIGN KEY (Id_Chambre)
REFERENCES CHAMBRE(Id_Chambre)

ALTER TABLE AYANT
ADD FOREIGN KEY (Code_Commodite)
REFERENCES COMMODITE(Code_Commodite)

ALTER TABLE ARRIVE
ADD FOREIGN KEY (Id_Reserve)
REFERENCES RESERVE(Id_Reserve)

ALTER TABLE ARRIVE
ADD FOREIGN KEY (Id_Client)
REFERENCES CLIENT(Id_Client)

ALTER TABLE ARRIVE
ADD FOREIGN KEY (Id_Chambre)
REFERENCES CHAMBRE(Id_Chambre)

ALTER TABLE DEPART
ADD FOREIGN KEY (Id_Arrive)
REFERENCES ARRIVE(Id_Arrive)

ALTER TABLE TRX
ADD FOREIGN KEY (Id_Arrive)
REFERENCES ARRIVE(ID_Arrive)


ALTER TABLE TRX
ADD FOREIGN KEY (Code_TypeTrx)
REFERENCES TYPETRX(Code_TypeTrx)


insert into CLIENT(NOM, ADRESSE, TELEPHONE, NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) 
VALUES ('FREDY BEAULIEU', '983 120E RUE', '7892645367',  '7892-1987-11101','VISA', GETDATE()+127, 0);
insert into CLIENT(Nom, Adresse, Telephone,  NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) 
VALUES ('EMILE BRETON', '123 AVENUE LUNAIRE', '1234098765',  '1234-5678-90123','VISA', GETDATE()+200, 199.29);
insert into CLIENT(NOM, ADRESSE, TELEPHONE,  NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) VALUES ('EMILE BRETON', '123 AVENUE LUNAIRE', '1234098765',  '1234-5678-90123','VISA', GETDATE()+200, 199.29);
insert into CLIENT(NOM, ADRESSE, TELEPHONE,  NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) VALUES ('STEPHANIE BELAND', '121 RUE SATURNE', '1234098765',  '1234-5678-90123','MASTERCARD', GETDATE()+300, 0);
insert into CLIENT(NOM, ADRESSE, TELEPHONE, NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) VALUES ('VINCENT BASTIEN', '312 RUE SOPHIA', '7832918473', '1232-5623-90154','MASTERCARD', GETDATE()+403, 0);
insert into CLIENT(NOM, ADRESSE, TELEPHONE,  NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) VALUES ('PASCAL GIRON', '3281 2E RUE', '7478918473', '3890083728', 'AMEX', GETDATE()+646, 0);
insert into CLIENT(NOM, ADRESSE, TELEPHONE, NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) VALUES ('SYLVIE DIAMOND', '112 RUE FIELD', '9348372675',  '9647-1678-78362','MASTERCARD', GETDATE()+378, 0);
insert into CLIENT(NOM, ADRESSE, TELEPHONE, NUM_CARTE, TYPE_CARTE, DATE_EXP, SOLDE_DU) VALUES ('FREDY BEAULIEU', '983 120E RUE', '7892645367',  '7892-1987-11101','VISA', GETDATE()+127, 0);


insert into RESERVE(ID_CLIENT, DATE_RESERVE, DATE_DEBUT, DATE_FIN, CONFIRME) VALUES (1, GETDATE() - 5, GETDATE() - 2, GETDATE() + 15, 0);
insert into RESERVE(ID_CLIENT, DATE_RESERVE, DATE_DEBUT, DATE_FIN, CONFIRME) VALUES (2, GETDATE() - 2, GETDATE() - 1, GETDATE() + 14, 0);
INSERT INTO RESERVE(Id_Client,Date_Reserve,Date_Debut ,Date_Fin ,Confirme) Values (3, GETDATE() - 0, GETDATE() - 2, GETDATE() +10, 0);
 
 INSERT INTO RESERVE (Id_Client ,Date_Reserve ,Date_Debut ,Date_Fin ,Confirme) values(1, GETDATE() - 3, GETDATE() + 13, GETDATE() + 16, 0);

 INSERT INTO RESERVE (Id_Client ,Date_Reserve ,Date_Debut ,Date_Fin ,Confirme) values(4, GETDATE() - 10, GETDATE() + 23, GETDATE() + 26, 0);

 INSERT INTO RESERVE (Id_Client ,Date_Reserve ,Date_Debut ,Date_Fin ,Confirme) values(5, GETDATE() - 8, GETDATE() - 1, GETDATE() + 7, 0);


 insert into COMMODITE(Code_Commodite,Description) VALUES ('AS', 'STANDARDS');
insert into COMMODITE(Code_Commodite,Description) VALUES ('MB', 'MINI BAR');
insert into COMMODITE(Code_Commodite,Description) values ('BA', 'AVEC BALCON');
insert into COMMODITE(Code_Commodite,Description) values ('BT', 'AVEC BAIN TOURBILLON');
insert into COMMODITE(Code_Commodite,Description) values ('CC', 'CHAMBRE COMMUNICANTE');
insert into COMMODITE(Code_Commodite,Description) values ('IN', 'INTERNET');
insert into COMMODITE(Code_Commodite,Description) values ('HP', 'HANDICAPE');
insert into COMMODITE(Code_Commodite,Description)values ('NF', 'NON FUMEUR');
insert into COMMODITE(Code_Commodite,Description)values ('CF', 'Machine à café');
insert into COMMODITE(Code_Commodite,Description) values ('HD', 'Téléviseur HD 42 pouces');

insert into LOCALISATION(CODE_LOCalisation , DESCRIPTION ) VALUES ( 'VM' , 'VUE SUR LA MER' );
insert into LOCALISATION(CODE_LOCalisation  , DESCRIPTION ) VALUES ( 'SM' , 'PRES DE LA SALLE A MANGER' );
insert into LOCALISATION(CODE_LOCalisation , DESCRIPTION ) VALUES ( 'AR' , 'ARRIERE' );
insert into LOCALISATION(CODE_LOCalisation  , DESCRIPTION ) VALUES ( 'ER' , 'PRES ESCALIERS ROULANTS' );
insert into LOCALISATION(CODE_LOCalisation  , DESCRIPTION ) VALUES ( 'AV' , 'AVANT' );


INSERT INTO TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo)VALUES ('1J', '1 LIT JUMEAU', 1);
insert into TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo) VALUeS ('LQ', 'LIT QUEEN', 1);
insert into TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo) VALUES ('2J', '2 LITS JUMEAUX',0);
insert into TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo) VALUES ('1D', '1 LIT DOUBLE', 0);
insert into TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo) VALUES ('2D', '2 LITS DOUBLES', 5);
insert into TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo) VALUES ('LK', 'LIT KING', 0);
insert into TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo) VALUES ('ST', 'SUITE', 0);
insert into TYPECHAMBRE(Code_TypeChambre,Description ,Nbr_Dispo) VALUES ('SR', 'SALLE RÉCEPTION', 0);

INSERT INTO CHAMBRE (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0101, 1, 200.10, 1, '1J','AV', 'fait froid ');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0201,2, 89.99, 1, 'LQ','SM', 'Belle vue');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0202,2, 130.99, 1, '2D','VM', 'vue sur mer');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0102,1, 78.99, 1, '2D','VM', 'ordinaire');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0103,1, 78.99, 1, '2D','VM', 'ordinaire');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0104,1, 78.99, 0, '2D','VM', 'ordinaire');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0301,3, 100.99, 1, '2D','VM', 'ordinaire');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0302,3, 100.99, 1, '2D','VM', 'ordinaire');
insert into CHAMBRE   (Id_Chambre,Etage,Prix,Etat,Code_TypeChambre,Code_Localisation,Memo) VALUES (0303,3, 100.99, 1, '2D','VM', 'ordinaire');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) values (0101, 'AS');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) VALUES (0202, 'MB');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) values (0201, 'AS');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) VALUES (0102, 'MB');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) values (0103, 'AS');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) VALUES (0104, 'MB');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) values (0301, 'AS');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) VALUES (0301, 'MB');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) values (0302, 'AS');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) VALUES (0302, 'MB');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) values (0303, 'AS');
insert into AYANT(ID_CHAMbre, CODE_COMmodite) VALUES (0303, 'MB');

insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (1, 0101, 1);
insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (2, 0201, 1);
insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (3, 0102, 0);
insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (3, 0103, 1);
insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (3, 301, 1);
insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (3, 302, 1);
insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (3, 303, 0);

insert into DE(ID_RESERve, ID_CHAMbre, ATTRIBUEE) VALUES (4, 0202, 0);
insert into DE(ID_RESERve, ID_CHAMbre,ATTRIBUEE) VALUES (5, 0202, 0);
insert into DE(ID_RESERve, ID_CHAMbre,ATTRIBUEE) VALUES (6, 0202, 1);

insert into ARRIVE(ID_RESERve, DATE_ARRIVe, ID_CLIENT, ID_CHAMbre, RECU_PAR ) VALUES(1, GETDATE(), 1, 0101, 'LA');
insert into ARRIVE(ID_RESERve, DATE_ARRIVe, ID_CLIENT,ID_CHAMbre, RECU_PAR ) VALUES(2, GETDATE()-1, 2,0201, 'LA');
insert into ARRIVE(ID_RESERve, DATE_ARRIVe, ID_CLIENT, ID_CHAMbre, RECU_PAR ) VALUES(3, GETDATE()-1, 4, 0103, 'LA');
insert into ARRIVE(ID_RESERve, DATE_ARRIVe, ID_CLIENT, ID_CHAMbre, RECU_PAR ) VALUES(3, GETDATE()-1, 4, 301, 'LA');
insert into ARRIVE(ID_RESERve, DATE_ARRIVe, ID_CLIENT, ID_CHAMbre, RECU_PAR ) VALUES(3, GETDATE()-1, 4, 302, 'LA');
insert into ARRIVE(ID_RESERve, DATE_ARRIVe, ID_CLIENT, ID_CHAMbre, RECU_PAR ) VALUES(6, GETDATE()-1, 5, 0202, 'LA');


insert into TYPETRX(Code_TypeTrx,Description,Db_Cr) VALUES ('01', 'PRIX DE LA CHAMBRE','DB');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr) VALUES ('02', 'LIT ADDITIONNEL','DB');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr)  VALUES ('03', 'TEL INTERURBAIN','DB');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr)  VALUES ('04', 'PHOTOCOPIE','DB');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr)  VALUES ('05', 'INTERNET','DB');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr) VALUES ('06', 'RESTAURANT','DB');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr)  VALUES ('07', 'BAR','DB');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr)  VALUES ('08', 'DEPOT ARGENT','CR');
insert into TYPETRX(Code_TypeTrx,Description,Db_Cr) VALUES ('09', 'PAIEMENT','CR');

INSERT INTO DEPART (Id_Arrive ,Date_Depart,Conf_Par)VALUES(4, GETDATE(),'LA');

insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee) VALUES(1,GetDate(),'01', 120,'SD','0');
insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee)VALUES(2,GetDate(),'02', 30,'SD','1');
insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee) VALUES(2,GetDate(),'06', 123,'SD','0');
insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee) VALUES(1,GetDate(),'08', 50,'SD','1');
insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee)  VALUES(1,GetDate(),'04', 56,'SD','0');
insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee) VALUES(3,GetDate(),'04', 56,'SD','0');
insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee) VALUES(2,GetDate(),'05', 95,'SD','0');
insert into TRX(Id_Arrive,Date_Trx,Code_TypeTrx,Montant,Conf_Par,Reportee) VALUES(1,GetDate(),'08', 50,'SD','1');
