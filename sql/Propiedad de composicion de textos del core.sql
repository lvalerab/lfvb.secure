SELECT * FROM prop_propiedad WHERE cod_prop='CORE_PROP'

SELECT * FROM prti_prop_tiel

SELECT * FROM tppr_tipo_propiedad

INSERT INTO prop_propiedad (COD_PROP, COD_PROP_PADRE, COD_TPPR, NOMBRE_PROP) VALUES ('CORE_TXID','CORE_PROP','TXT','Textos fijos de la aplicación');
INSERT INTO prti_prop_tiel (COD_TIEL, COD_PROP) VALUES ('core','CORE_TXID');

INSERT INTO prop_propiedad (COD_PROP, COD_PROP_PADRE, COD_TPPR, NOMBRE_PROP) VALUES ('CRTX_CLTX','CORE_TXID','LISTAFIJ','Colección de textos donde se configurará los textos fijos de la aplicación');
INSERT INTO prti_prop_tiel (COD_TIEL, COD_PROP) VALUES ('core','CRTX_CLTX');
INSERT INTO povs_prop_valores_sql (COD_PROP, ETIQUETA_POVS, FILTRO_POR_ID, SQL_POVS) VALUES ('CRTX_CLTX','Colecciones de textos','S','SELECT ID_CLTX AS value, NOMBRE_CLTX AS label FROM cltx_coleccion_texto');

INSERT INTO prop_propiedad (COD_PROP, COD_PROP_PADRE, COD_TPPR, NOMBRE_PROP) VALUES ('CRTX_TITU','CORE_TXID','LISTAFIJ','Campo para el titulo de la aplicación');
INSERT INTO prti_prop_tiel (COD_TIEL, COD_PROP) VALUES ('core','CRTX_TITU');
INSERT INTO povs_prop_valores_sql (COD_PROP, ETIQUETA_POVS, FILTRO_POR_ID, SQL_POVS) VALUES ('CRTX_TITU','Campo de la colección a usar','S','SELECT cmtx.ID_CMTX AS VALUE, cmtx.NOMBRE_CMTX AS label FROM cmtx_campo_texto cmtx INNER JOIN  vrep_valor_elpr vrep ON cmtx.ID_CLTX=vrep.VALOR_TEXTO INNER JOIN elpr_elemento_propiedad elpr ON vrep.ID_ELPR=elpr.ID_ELPR INNER JOIN elem_elemento elem ON elpr.ID_ELEM=elem.ID_ELEM WHERE elem.COD_TIEL=''core'' AND cod_prop=''CRTX_CLTX''');




