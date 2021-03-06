PGDMP     /    (                y         	   turismorc    13.2    13.2 %    ?           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ?           1262    42342 	   turismorc    DATABASE     h   CREATE DATABASE turismorc WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Colombia.1252';
    DROP DATABASE turismorc;
                postgres    false                        2615    43814    tours    SCHEMA        CREATE SCHEMA tours;
    DROP SCHEMA tours;
                postgres    false                        2615    42343    usuarios    SCHEMA        CREATE SCHEMA usuarios;
    DROP SCHEMA usuarios;
                postgres    false            ?            1259    43835 	   municipio    TABLE     ^   CREATE TABLE tours.municipio (
    id integer NOT NULL,
    nombre_municipio text NOT NULL
);
    DROP TABLE tours.municipio;
       tours         heap    postgres    false    4            ?            1259    43833    municipio_id_seq    SEQUENCE     ?   CREATE SEQUENCE tours.municipio_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE tours.municipio_id_seq;
       tours          postgres    false    4    209            ?           0    0    municipio_id_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE tours.municipio_id_seq OWNED BY tours.municipio.id;
          tours          postgres    false    208            ?            1259    43817    tour    TABLE     ?   CREATE TABLE tours.tour (
    id integer NOT NULL,
    nombre_del_sitio text NOT NULL,
    municipio_id integer NOT NULL,
    descripcion_tour json NOT NULL,
    precio double precision NOT NULL,
    path_imagen text
);
    DROP TABLE tours.tour;
       tours         heap    postgres    false    4            ?            1259    43815    tour_id_seq    SEQUENCE     ?   CREATE SEQUENCE tours.tour_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 !   DROP SEQUENCE tours.tour_id_seq;
       tours          postgres    false    207    4            ?           0    0    tour_id_seq    SEQUENCE OWNED BY     9   ALTER SEQUENCE tours.tour_id_seq OWNED BY tours.tour.id;
          tours          postgres    false    206            ?            1259    42357    roles    TABLE     \   CREATE TABLE usuarios.roles (
    id integer NOT NULL,
    descripcion_rol text NOT NULL
);
    DROP TABLE usuarios.roles;
       usuarios         heap    postgres    false    7            ?            1259    42355    roles_id_seq    SEQUENCE     ?   CREATE SEQUENCE usuarios.roles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE usuarios.roles_id_seq;
       usuarios          postgres    false    7    205            ?           0    0    roles_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE usuarios.roles_id_seq OWNED BY usuarios.roles.id;
          usuarios          postgres    false    204            ?            1259    42346    usuario    TABLE     '  CREATE TABLE usuarios.usuario (
    id integer NOT NULL,
    nombre_completo text NOT NULL,
    password text NOT NULL,
    email text NOT NULL,
    cedula text NOT NULL,
    rol_id integer NOT NULL,
    token text,
    vencimiento_token timestamp without time zone,
    municipio_id integer
);
    DROP TABLE usuarios.usuario;
       usuarios         heap    postgres    false    7            ?            1259    42344    usuario_id_seq    SEQUENCE     ?   CREATE SEQUENCE usuarios.usuario_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE usuarios.usuario_id_seq;
       usuarios          postgres    false    7    203            ?           0    0    usuario_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE usuarios.usuario_id_seq OWNED BY usuarios.usuario.id;
          usuarios          postgres    false    202            =           2604    43838    municipio id    DEFAULT     j   ALTER TABLE ONLY tours.municipio ALTER COLUMN id SET DEFAULT nextval('tours.municipio_id_seq'::regclass);
 :   ALTER TABLE tours.municipio ALTER COLUMN id DROP DEFAULT;
       tours          postgres    false    209    208    209            <           2604    43820    tour id    DEFAULT     `   ALTER TABLE ONLY tours.tour ALTER COLUMN id SET DEFAULT nextval('tours.tour_id_seq'::regclass);
 5   ALTER TABLE tours.tour ALTER COLUMN id DROP DEFAULT;
       tours          postgres    false    206    207    207            ;           2604    42360    roles id    DEFAULT     h   ALTER TABLE ONLY usuarios.roles ALTER COLUMN id SET DEFAULT nextval('usuarios.roles_id_seq'::regclass);
 9   ALTER TABLE usuarios.roles ALTER COLUMN id DROP DEFAULT;
       usuarios          postgres    false    205    204    205            :           2604    42349 
   usuario id    DEFAULT     l   ALTER TABLE ONLY usuarios.usuario ALTER COLUMN id SET DEFAULT nextval('usuarios.usuario_id_seq'::regclass);
 ;   ALTER TABLE usuarios.usuario ALTER COLUMN id DROP DEFAULT;
       usuarios          postgres    false    202    203    203            ?          0    43835 	   municipio 
   TABLE DATA           8   COPY tours.municipio (id, nombre_municipio) FROM stdin;
    tours          postgres    false    209   ?&       ?          0    43817    tour 
   TABLE DATA           h   COPY tours.tour (id, nombre_del_sitio, municipio_id, descripcion_tour, precio, path_imagen) FROM stdin;
    tours          postgres    false    207    '       ?          0    42357    roles 
   TABLE DATA           6   COPY usuarios.roles (id, descripcion_rol) FROM stdin;
    usuarios          postgres    false    205   ?(       ?          0    42346    usuario 
   TABLE DATA           ?   COPY usuarios.usuario (id, nombre_completo, password, email, cedula, rol_id, token, vencimiento_token, municipio_id) FROM stdin;
    usuarios          postgres    false    203   ?(       ?           0    0    municipio_id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('tours.municipio_id_seq', 3, true);
          tours          postgres    false    208            ?           0    0    tour_id_seq    SEQUENCE SET     8   SELECT pg_catalog.setval('tours.tour_id_seq', 4, true);
          tours          postgres    false    206            ?           0    0    roles_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('usuarios.roles_id_seq', 5, true);
          usuarios          postgres    false    204            ?           0    0    usuario_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('usuarios.usuario_id_seq', 1, true);
          usuarios          postgres    false    202            E           2606    43843    municipio municipio_pkey 
   CONSTRAINT     U   ALTER TABLE ONLY tours.municipio
    ADD CONSTRAINT municipio_pkey PRIMARY KEY (id);
 A   ALTER TABLE ONLY tours.municipio DROP CONSTRAINT municipio_pkey;
       tours            postgres    false    209            C           2606    43825    tour tour_pkey 
   CONSTRAINT     K   ALTER TABLE ONLY tours.tour
    ADD CONSTRAINT tour_pkey PRIMARY KEY (id);
 7   ALTER TABLE ONLY tours.tour DROP CONSTRAINT tour_pkey;
       tours            postgres    false    207            A           2606    42365    roles roles_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY usuarios.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY usuarios.roles DROP CONSTRAINT roles_pkey;
       usuarios            postgres    false    205            ?           2606    42354    usuario usuario_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY usuarios.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY usuarios.usuario DROP CONSTRAINT usuario_pkey;
       usuarios            postgres    false    203            H           2606    43844    tour FK_municipio_id    FK CONSTRAINT     ?   ALTER TABLE ONLY tours.tour
    ADD CONSTRAINT "FK_municipio_id" FOREIGN KEY (municipio_id) REFERENCES tours.municipio(id) NOT VALID;
 ?   ALTER TABLE ONLY tours.tour DROP CONSTRAINT "FK_municipio_id";
       tours          postgres    false    2885    207    209            G           2606    43932    usuario FK_municipio_id    FK CONSTRAINT     ?   ALTER TABLE ONLY usuarios.usuario
    ADD CONSTRAINT "FK_municipio_id" FOREIGN KEY (municipio_id) REFERENCES tours.municipio(id) NOT VALID;
 E   ALTER TABLE ONLY usuarios.usuario DROP CONSTRAINT "FK_municipio_id";
       usuarios          postgres    false    2885    203    209            F           2606    42366    usuario FK_rol_id    FK CONSTRAINT        ALTER TABLE ONLY usuarios.usuario
    ADD CONSTRAINT "FK_rol_id" FOREIGN KEY (rol_id) REFERENCES usuarios.roles(id) NOT VALID;
 ?   ALTER TABLE ONLY usuarios.usuario DROP CONSTRAINT "FK_rol_id";
       usuarios          postgres    false    2881    203    205            ?   3   x?3?tKLN,I,?,;??ˈӭ?81=??4?3???/.,M-J?????? `??      ?   f  x?e??N?0E??W???j?Ja?R^?t???X?T?'??(??I(????;>??iv?t_???j?.a??Z?Y?}?km(D???ڥ?$O???"???????a?????)?:???0?D?*j?GR(k?N????^	??3h?(?K?4????J?"(J5????U?&??bXU??܂)?c?v??u?C?M?LBu?;n??؁??n?~?{????`?1??{/S???"??????;W?$?l?zj$` Lod?;<i?$???JQ????t???YZ?rl???s;*n?]?}??M?????`wu??`K?
??(?̊I7???\I??Ø?a??????Iq2????T??`?o???      ?   O   x?3?.-H-rL????,.)JL?/?2?D????e&g$?ps??f&r?p?d??$e?s?r:?d?敤r??qqq ??2      ?   S   x?3?L?LM?WH,*)-?W(H,*J?,?KI-*I?N-24????;d???&f??%??r?Ypq????!W? q?!     