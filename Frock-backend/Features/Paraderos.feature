Feature: Gestión de paraderos
  Como gestor de la empresa de transporte  
  Quiero crear, listar, editar y eliminar paraderos  
  Para mantener mi sistema con paraderos válidos y actualizados

  Background:
    Given la API está disponible  
    And estoy autenticado como gestor de la empresa

  Scenario: Crear paradero exitoso
    When envío POST a "/api/stops" con:
      | campo     | valor               |
      | nombre    | Paradero Centro     |
      | region    | Lima                |
      | provincia | Lima                |
      | distrito  | Miraflores          |
      | localidad | Zona A              |
      | direccion | Av. Central 123     |
      | referencia| Frente al parque    |
    Then recibo HTTP 201 con { id: <nuevoId>, message: "Paradero creado" }

  Scenario: Listar paraderos existentes
    Given existen los siguientes paraderos:
      | id | nombre           |
      | 1  | Paradero Centro  |
      | 2  | Paradero Norte   |
    When envío GET a "/api/stops"
    Then recibo HTTP 200 y la respuesta contiene esos paraderos

  Scenario: Editar paradero exitoso
    Given existe un paradero con id 1  
    When envío PUT a "/api/stops/1" con:
      | campo   | valor                 |
      | nombre  | Paradero Actualizado  |
      | direccion | Av. Nueva 456       |
    Then recibo HTTP 200 y { message: "Paradero actualizado" }

  Scenario: Eliminar paradero exitoso
    Given existe un paradero con id 2  
    When envío DELETE a "/api/stops/2"
    Then recibo HTTP 204

  Scenario: Eliminar paradero inexistente
    When envío DELETE a "/api/stops/999"
    Then recibo HTTP 404 y { error: "Paradero no encontrado" }