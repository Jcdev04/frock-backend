# Features/ParaderosViajero.feature
Feature: Exploración de paraderos para viajeros
  Como viajero de la plataforma
  Quiero filtrar paraderos por región, provincia, distrito y localidad
  Para ubicar los paraderos que necesito en esa ubicación

  Background:
    Given la API está disponible

  Scenario Outline: Filtrar paraderos por ubicación
    When envío GET a "/api/stops?region=<region>&provincia=<provincia>&distrito=<distrito>&localidad=<localidad>"
    Then recibo HTTP 200
    And la respuesta contiene únicamente paraderos ubicados en <region>, <provincia>, <distrito> y <localidad>

    Examples:
      | region | provincia | distrito   | localidad |
      | Lima   | Lima      | Miraflores | Zona A    |
      | Junín  | Huancayo  | El Tambo   | Centro    |

  Scenario: Ver paraderos en forma de tarjetas resumidas
    Given que ya apliqué un filtro válido
    When envío GET a "/api/stops?region=Lima&provincia=Lima&distrito=Miraflores&localidad=Zona A"
    Then recibo HTTP 200
    And la respuesta contiene una lista de objetos donde cada objeto incluye:
      | id         |
      | nombre     |
      | direccion  |
      | referencia |
      | ruta       |

  Scenario: Navegar a la página de detalle de un paradero
    Given existe un paradero con id 5
    When envío GET a "/api/stops/5"
    Then recibo HTTP 200
    And la respuesta contiene los siguientes campos completos:
      | id         |
      | nombre     |
      | direccion  |
      | referencia |
      | region     |
      | provincia  |
      | distrito   |
      | localidad  |
      | ruta       |
