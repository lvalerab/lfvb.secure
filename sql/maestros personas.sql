
SELECT * FROM sitp_tipo_situacion_persona

ALTER TABLE trpr_tipo_relacion_persona ADD COLUMN COD_TRPR_RECIPROCO VARCHAR(36) DEFAULT NULL;
ALTER TABLE trpr_tipo_relacion_persona ADD CONSTRAINT FK_TRPR_TRPR FOREIGN KEY (COD_TRPR_RECIPROCO) REFERENCES trpr_tipo_relacion_persona (COD_TRPR);

INSERT INTO sitp_tipo_situacion_persona (COD_SITP, NOMBRE_SITP) VALUES ('NAC','Nacimiento');
INSERT INTO sitp_tipo_situacion_persona (COD_SITP, NOMBRE_SITP) VALUES ('ADP','Adopcion');
INSERT INTO sitp_tipo_situacion_persona (COD_SITP, NOMBRE_SITP) VALUES ('EMP','Empadronado');
INSERT INTO sitp_tipo_situacion_persona (COD_SITP, NOMBRE_SITP) VALUES ('CAS','Casamiento legal');
INSERT INTO sitp_tipo_situacion_persona (COD_SITP, NOMBRE_SITP) VALUES ('SEP','Separacion legal');
INSERT INTO sitp_tipo_situacion_persona (COD_SITP, NOMBRE_SITP) VALUES ('DIV','Divorcio legal');
INSERT INTO sitp_tipo_situacion_persona (COD_SITP, NOMBRE_SITP) VALUES ('DEF','Defunción');


INSERT INTO tiid_tipo_identificador_persona (COD_TIID, NOMBRE_TIID) VALUES ('NIF','NIF/DNI español');
INSERT INTO tiid_tipo_identificador_persona (COD_TIID, NOMBRE_TIID) VALUES ('NIE','NIE español');
INSERT INTO tiid_tipo_identificador_persona (COD_TIID, NOMBRE_TIID) VALUES ('PASSPORT','Pasaporte');


INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('HIJO','Hijo/a de la persona');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('PROG','Padre/Madre de la persona');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('CONY','Conyuge de la persona - Situación legal no separado-');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('CONS','Conyuge sepearado de la persona - Situación legal separado/divorciado-');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('ADM','Administrador/a de la entidad jurídica');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('TES','Tesorero/a de la entidad jurídica');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('AEJ','Relacionado con la entidad juridica');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('TIO','Tio/a de la persona');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('PRIM','Primo/a de la persona');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('2GAF','2º Grado de afinidad');
INSERT INTO trpr_tipo_relacion_persona (COD_TRPR, NOMBRE_REPE) VALUES ('3GAF','3º y demas Grados de afinidad');