PGDMP                         y         	   turismorc    13.2    13.2     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    42342 	   turismorc    DATABASE     h   CREATE DATABASE turismorc WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Colombia.1252';
    DROP DATABASE turismorc;
                postgres    false                        2615    42343    usuarios    SCHEMA        CREATE SCHEMA usuarios;
    DROP SCHEMA usuarios;
                postgres    false            �            1259    42357    roles    TABLE     \   CREATE TABLE usuarios.roles (
    id integer NOT NULL,
    descripcion_rol text NOT NULL
);
    DROP TABLE usuarios.roles;
       usuarios         heap    postgres    false    6            �            1259    42355    roles_id_seq    SEQUENCE     �   CREATE SEQUENCE usuarios.roles_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 %   DROP SEQUENCE usuarios.roles_id_seq;
       usuarios          postgres    false    6    204            �           0    0    roles_id_seq    SEQUENCE OWNED BY     A   ALTER SEQUENCE usuarios.roles_id_seq OWNED BY usuarios.roles.id;
          usuarios          postgres    false    203            �            1259    42346    usuario    TABLE       CREATE TABLE usuarios.usuario (
    id integer NOT NULL,
    nombre_completo text NOT NULL,
    password text NOT NULL,
    email text NOT NULL,
    cedula text NOT NULL,
    rol_id integer NOT NULL,
    token text,
    vencimiento_token timestamp without time zone
);
    DROP TABLE usuarios.usuario;
       usuarios         heap    postgres    false    6            �            1259    42344    usuario_id_seq    SEQUENCE     �   CREATE SEQUENCE usuarios.usuario_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE usuarios.usuario_id_seq;
       usuarios          postgres    false    202    6            �           0    0    usuario_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE usuarios.usuario_id_seq OWNED BY usuarios.usuario.id;
          usuarios          postgres    false    201            ,           2604    42360    roles id    DEFAULT     h   ALTER TABLE ONLY usuarios.roles ALTER COLUMN id SET DEFAULT nextval('usuarios.roles_id_seq'::regclass);
 9   ALTER TABLE usuarios.roles ALTER COLUMN id DROP DEFAULT;
       usuarios          postgres    false    204    203    204            +           2604    42349 
   usuario id    DEFAULT     l   ALTER TABLE ONLY usuarios.usuario ALTER COLUMN id SET DEFAULT nextval('usuarios.usuario_id_seq'::regclass);
 ;   ALTER TABLE usuarios.usuario ALTER COLUMN id DROP DEFAULT;
       usuarios          postgres    false    201    202    202            �          0    42357    roles 
   TABLE DATA           6   COPY usuarios.roles (id, descripcion_rol) FROM stdin;
    usuarios          postgres    false    204   x       �          0    42346    usuario 
   TABLE DATA           s   COPY usuarios.usuario (id, nombre_completo, password, email, cedula, rol_id, token, vencimiento_token) FROM stdin;
    usuarios          postgres    false    202   �       �           0    0    roles_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('usuarios.roles_id_seq', 5, true);
          usuarios          postgres    false    203            �           0    0    usuario_id_seq    SEQUENCE SET     >   SELECT pg_catalog.setval('usuarios.usuario_id_seq', 1, true);
          usuarios          postgres    false    201            0           2606    42365    roles roles_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY usuarios.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY usuarios.roles DROP CONSTRAINT roles_pkey;
       usuarios            postgres    false    204            .           2606    42354    usuario usuario_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY usuarios.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY usuarios.usuario DROP CONSTRAINT usuario_pkey;
       usuarios            postgres    false    202            1           2606    42366    usuario FK_rol_id    FK CONSTRAINT        ALTER TABLE ONLY usuarios.usuario
    ADD CONSTRAINT "FK_rol_id" FOREIGN KEY (rol_id) REFERENCES usuarios.roles(id) NOT VALID;
 ?   ALTER TABLE ONLY usuarios.usuario DROP CONSTRAINT "FK_rol_id";
       usuarios          postgres    false    204    202    2864            �   O   x�3�.-H-rL����,.)JL�/�2�D����e&g$�ps��f&r�p�d��$e�s�r:�d�敤r��qqq ��2      �   R   x�3�L�LM�WH,*)-�W(H,*J�,�KI-*I�N-24����;d��&f��%��r�Yp�r��W� B�     