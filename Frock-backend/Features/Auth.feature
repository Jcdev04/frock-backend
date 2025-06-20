Feature: Autenticación de usuarios
  En la plataforma de transporte, gestionar el registro, login y logout de usuarios

  Background:
    Given la API está disponible

  Scenario: Registro exitoso
    Given que no existe un usuario con email "u@dominio.com"
    When envío POST a "/api/register" con:
      | email    | u@dominio.com |
      | password | MiP4ssw0rd!   |
      | rol      | Cliente       |
    Then recibo HTTP 201 y { message: "Registro exitoso" }

  Scenario: Inicio de sesión exitoso
    Given que existe un usuario con email "u@dominio.com" y password "MiP4ssw0rd!"
    When envío POST a "/api/login" con:
      | email    | u@dominio.com   |
      | password | MiP4ssw0rd!     |
    Then recibo HTTP 200 y un token JWT

  Scenario: Cierre de sesión
    Given que estoy logueado con un token válido
    When envío POST a "/api/logout" con el token en la cabecera
    Then recibo HTTP 204
    And el token ya no es válido para llamadas protegidas
