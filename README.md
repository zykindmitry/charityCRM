# charityCRM
Free CRM for charity funds

Software architecture
------------------------

Key software quality criterias priority:

1. Security (we store personal data of people in potential danger)
2. Compatibility with varios OS and devices (due to DevFactoryZ's needs)
3. Maintainability exept reusability (we develop this project involving junior developers, no stable team and no test team at all)
4. Stability and functional suitability
5. Productivity and uability
6. Adaptivity and compatibity with other software

Interface requirements
-------------------------
Software application language: Russian

Coding style convention
-------------------------

Programming language: C#

NAMING:
  No abbrevations allowed exept:
  CRM,UI,INT,CHAR,BOOL
  
  No special characters or digits allowed
  
  Omit the keyword "this" is possible
  
  IDENFIERS:
    class - Pascal
    struct - Pascal
    interface - Pascal, starts with capital I, it's recommended to treat I as first person pronoun (example ICanDo, IAllowCopy etc.)
    fields - camel
    delegates - Pascal
    properties - Pascal
    methods - Pascal
    parameters - camel
    local variables - camel
    public or internal constants - Pascal

FORMATTING:

   Use standard c# code blocks style:
   
   (block statement) line break { line break, four spaces, inner statement, line break, }
    
   possible exclusions:
   Simple properties formatting can be simplified (e.g. public string Name { get; set; } )
   
SPACING
   
   Each statement should be on its own line
   There should be an additional line above and below: property, method, constructor, event  
   
IN-CLASS GROUPING

   A code within class should be grouped based on functionality (not member type)
   
WRAPPING

  Different statements should not be on the same line
  Statement should not be wrapped unless its length exceeds 100 characters (including spaces)
  Wraping is allowed after the following tokens:
  parameter identifier, round brakets, class or type identifier
  If wrapping after a certain token needed then one should do wraping after each same token at the same level and all tokens of higher     level
  
