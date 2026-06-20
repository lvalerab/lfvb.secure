CREATE TABLE caus_calendario_usuario (ID_CAUS VARCHAR(36) PRIMARY KEY,
												  ID_USUA VARCHAR(36),
												  NOMBRE_CAUS VARCHAR(60));

ALTER TABLE caus_calendario_usuario ADD CONSTRAINT FK_CAUS_ELEM
												FOREIGN KEY (ID_CAUS) 
												REFERENCES elem_elemento (ID_ELEM);														
												
INSERT INTO tiel_tipo_elemento (COD_TIEL, NOMBRE_TIEL) VALUES ('caus', 'Calendario de usuario');																								  												

ALTER TABLE caus_calendario_usuario ADD CONSTRAINT FK_CAUS_USUA
												FOREIGN KEY (ID_USUA) 
												REFERENCES usua_usuarios (ID_USUA);
												



CREATE TABLE tenc_tipo_entrada_calendario (id_tenc VARCHAR(36) PRIMARY KEY,
														cod_tenc VARCHAR(10),
														nombre_tenc VARCHAR(60));
														
ALTER TABLE tenc_tipo_entrada_calendario ADD CONSTRAINT FK_TENC_ELEM
												FOREIGN KEY (ID_TENC) 
												REFERENCES elem_elemento (ID_ELEM);														
												
INSERT INTO tiel_tipo_elemento (COD_TIEL, NOMBRE_TIEL) VALUES ('tenc', 'Tipo entrada de calendario de usuario');


														
CREATE TABLE encl_entrada_calendario (ID_ENCL VARCHAR(36) PRIMARY KEY,
												  ID_TENC VARCHAR(36),
												  FECHA_INICIO_ENCL DATETIME,
												  FECHA_FIN_ENCL DATETIME,
												  ID_USER_CREADOR VARCHAR(36),
												  TITULO_ENCL VARCHAR(60),
												  DESCRIPCION_ENCL TEXT);
												  
ALTER TABLE encl_entrada_calendario ADD CONSTRAINT FK_ENCL_ELEM
												FOREIGN KEY (ID_ENCL) 
												REFERENCES elem_elemento (ID_ELEM);														
												
INSERT INTO tiel_tipo_elemento (COD_TIEL, NOMBRE_TIEL) VALUES ('encl', 'Entrada de calendario de usuario');

ALTER TABLE encl_entrada_calendario ADD CONSTRAINT FK_ENCL_TENC
												FOREIGN KEY (ID_TENC) 
												REFERENCES tenc_tipo_entrada_calendario (ID_TENC);	
												
ALTER TABLE encl_entrada_calendario ADD CONSTRAINT FK_ENCL_USUA
												FOREIGN KEY (ID_USER_CREADOR) 
												REFERENCES usua_usuarios (ID_USUA);													
												
												
CREATE TABLE paec_participantes_encl (ID_PAEC VARCHAR(36) PRIMARY key,
												  ID_ENCL VARCHAR(36),
												  ID_ELEM VARCHAR(36),
												  MAIL_PAEC VARCHAR(255));
												  

ALTER TABLE paec_participantes_encl ADD CONSTRAINT FK_PAEC_ELEM
												FOREIGN KEY (ID_PAEC) 
												REFERENCES elem_elemento (ID_ELEM);														
												
INSERT INTO tiel_tipo_elemento (COD_TIEL, NOMBRE_TIEL) VALUES ('paec', 'Participante Entrada de calendario de usuario');

ALTER TABLE paec_participantes_encl ADD CONSTRAINT FK_PAEC_ENCL
												FOREIGN KEY (ID_ENCL) 
												REFERENCES encl_entrada_calendario (ID_ENCL);														
												
ALTER TABLE paec_participantes_encl ADD CONSTRAINT FK_PAEC_PART
												FOREIGN KEY (ID_ELEM) 
												REFERENCES elem_elemento (ID_ELEM);	
												

CREATE TABLE elec_elemento_encl (ID_ENCL VARCHAR(36), ID_ELEM VARCHAR(36), DATOS_ELEC TEXT);

ALTER TABLE elec_elemento_encl ADD CONSTRAINT PRIMARY KEY (ID_ENCL, ID_ELEM);

ALTER TABLE elec_elemento_encl ADD CONSTRAINT FK_ELEC_ELEM
												FOREIGN KEY (ID_ELEM) 
												REFERENCES elem_elemento (ID_ELEM);	


																																					