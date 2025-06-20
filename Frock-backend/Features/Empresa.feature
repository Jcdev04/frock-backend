Feature: Gestión de datos de la empresa
  Como gestor de la empresa de transporte  
  Quiero registrar y editar los datos generales de mi compañía  
  Para que esa información quede guardada y siempre actualizada

  Background:
    Given la API está disponible  
    And estoy autenticado como gestor de la empresa

  Scenario: Registro exitoso de datos generales
    Given es mi primer inicio de sesión  
    When envío POST a "/api/company" con:
      | campo     | valor              |
      | nombre    | Empresa XYZ        |
      | ruc       | 12345678901        |
      | direccion | Av. Principal 123  |
      | telefono  | 987654321          |
      | logoUrl   | http://logo.png    |
    Then recibo HTTP 201 y { message: "Empresa registrada" }

  Scenario: Registro con RUC duplicado
    Given existe una empresa con RUC "12345678901"  
    When envío POST a "/api/company" con:
      | campo | valor        |
      | ruc   | 12345678901  |
    Then recibo HTTP 400 y { error: "RUC ya registrado" }

  Scenario: Edición exitosa de datos de la empresa
    When envío PUT a "/api/company" con:
      | campo   | valor           |
      | nombre  | Nueva Empresa   |
      | telefono| 990000000       |
    Then recibo HTTP 200 y { message: "Actualización exitosa" }

  Scenario: Edición con RUC duplicado
    Given existe otra empresa con RUC "99999999999"  
    When envío PUT a "/api/company" con:
      | campo | valor         |
      | ruc   | 99999999999   |
    Then recibo HTTP 400 y { error: "RUC ya registrado" }