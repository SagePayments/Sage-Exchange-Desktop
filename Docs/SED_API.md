# SED 2.0 API Specifications
This document defines the objects and methods that make up the SED 2.0 API. It also includes sample code in C++, C#, and VB6.

1. [Implementation](#Implementation)
1. [Objects](#Objects)
1. [IModuleClient Members](#IModuleClient)
1. [IModuleClient2 Members](#IModuleClient2)
1. [IModuleResponse Members](#IModuleResponse)
1. [C++ Sample](#C++)
1. [ModuleClient Members](#ModuleClient)
1. [ModuleResponse Members](#ModuleResponse)
1. [C# Sample](#Csharp)
1. [VB 6 Sample](#VB6)
1. [SED Payment Process Flow](#FlowDiagram)

## <a name="Implementation"></a> Implementation

The Sage Exchange Desktop v2.0 installation path is \[Program Files\]\\Sage Payment Solutions\\Sage Exchange Desktop API\\clSedApi.dll.

## <a name="Objects"></a> Objects

The following objects/interfaces represent implementations of the Sage Exchange Desktop API. The API provides both COM and .Net objects. When using .Net simply add this library as a class library reference to your project/solution.

### CLSID

| Item           | Registry Key                         |
|----------------|--------------------------------------|
| ModuleClient   | CF5D885D-7838-4807-A2BA-BB0D92D9B1EA |
| ModuleResponse | AF364412-E730-4738-91BB-B73124B96633 |

### PROGID

| Item           | Key                     |
|----------------|-------------------------|
| ModuleClient   | clSedApi.ModuleClient   |
| ModuleResponse | clSedApi.ModuleResponse |

### COM Objects/Interfaces

| Item            | Description                                                                            |
|-----------------|----------------------------------------------------------------------------------------|
| IModuleClient   | Provides client-side communication to the Payment Module.                              |
| IModuleClient2  | This provides additional client-side communication to the Payment Module               |
| IModuleResponse | Represents a Payment Module response and is the interface for accessing response data. |

### .Net Objects

| Item           | Description                                                                            |
|----------------|----------------------------------------------------------------------------------------|
| ModuleClient   | Provides client-side communication to the Payment Module.                              |
| ModuleResponse | Represents a Payment Module response and is the interface for accessing response data. |

## <a name="IModuleClient"></a> IModuleClient Members

The following table shows the methods.

In C++, this interface inherits from IDispatch

| Method       | Description                                                     |
|--------------|-----------------------------------------------------------------|
| GetUniqueId  | Returns a Globally Unique Identifier.                           |
| GetUniqueIds | Returns an array of Globally Unique Identifiers.                |
| GetResponse  | Sends data to the Payment Module and returns the response data. |

### GetUniqueId

Gets a Globally Unique Identifier GUID to be assign to a transaction for use later in transaction status queries.

#### C++ Syntax
```c++
HRESULT GetUniqueId(*BSTR guid);
```
### GetUniqueIds

Gets an array of Globally Unique Identifiers GUIDs to be assigned to transactions for use later in transaction status queries.

#### C++ Syntax
```c++
HRESULT GetUniqueIds(long numberOfIds, SAFEARRAY** guids);
```
### GetResponse

Sends XML encoded string data to the Payment Module for processing and returns a Payment Module Response object. Refer to the *Sage Exchange XML Messaging Guide* for reference of the XML structures and formats. This method will block until completion, timeout, or error.

#### C++ Syntax
```c++
HRESULT GetResponse(BSTR request, IModuleResponse* response);
```
## <a name="IModuleClient2"></a> IModuleClient2 Members

The following table shows the methods.

In C++, this interface inherits from Idispatch

| Method               | Description                                                                                                                             |
|----------------------|-----------------------------------------------------------------------------------------------------------------------------------------|
| SetInstallPromptFlag | This method accepts a boolean value. If it is false, the Sage Exchange Desktop install prompt is suppressed. The default value is true. |

### SetInstallPromptFlag

#### C++ Syntax
```c++
HRESULT SetInstallPromptFlag (bool flag);
```
## <a name="IModuleResponse"></a> IModuleResponse Members

The following tables show the methods.

In C++, this interface inherits from IDispatch

### Methods

| Method               | Description                                           |
|----------------------|-------------------------------------------------------|
| GetStatusCode        | Returns the communication status code:<ul><li>0 = Success</li><li>1 = Invalid Request Data</li><li>2 = Communication Startup Error</li><li>3 = Communication Error</li><li>4 = Module Configuration Not Found</li><li>5 = Communication Recovery Error</li><li>6 = User Canceled</li><li>7 = Timeout</li><li>8 = Internet Connection Error</li><li>9 = Unknown Error</li><li>10 = Remote Data Access Error</li><li>11 = Update In Progress</li></ul>|
| GetStatusDescription | Returns the communication status description          |
| GetResponseText      | Returns the XML encoded response string               |
| GetResponseLength    | Returns the length of the XML encoded response string |

### GetStatusCode

Returns the communication status code. A value of zero means success, a non-zero value indicates an error was encountered communicating with the Payment Module.

#### C++ Syntax
```c++
HRESULT GetStatusCode(long* statusCode );
```
### GetStatusDescription

Returns the communication status textual description.

#### C++ Syntax
```c++
HRESULT GetStatusDescription(BSTR* statusDescription );
```
### GetResponseText

Returns the XML encoded response string data from the Payment Module.

#### C++ Syntax
```c++
HRESULT GetResponseText(BSTR* responseText );
```
### GetResponseLength

Returns the string length of the XML encoded response.

#### C++ Syntax
```c++
HRESULT GetResponseLength(long* responseLength );
```
**Note:** Send an “ACK” string to the Sage Exchange Desktop and it will return an “ACK”. This is useful in determining if the Sage Exchange Desktop is running.

## <a name="C++"></a> C++ Sample

```cpp

  #import “clSedApi.tlb” no_namespace, raw_interfaces_only

  void Process()
  {

    CoInitialize(0);

    HRESULT hr;
    IModuleClientPtr pMC(__uuidof(ModuleClient));
    IModuleResponsePtr pMR;

    IModuleClient2Ptr pMC2;
    pMC2 = pMC;
    pMC2->SetInstallPromptFlag(false);

    _bstr_t xmlReq;
    BSTR bstrXmlResp = NULL;
    BSTR bstrUti = NULL;
    long statusCode = 0;
    hr = pMC->GetUniqueID(&bstrUti);

    if( FAILED(hr) )
    {
    	// handle COM error
    }

    if( bstrUti )
    	::SysFreeString(bstrUti);

    xmlReq = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
    xmlReq += "<Request_v1 xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">";
    xmlReq += "  <Application>";
    xmlReq += "    <ApplicationID>DEMO</ApplicationID>";
    xmlReq += "    <LanguageID>EN</LanguageID>";
    xmlReq += "  </Application>";
    xmlReq += "  <Payments>";
    xmlReq += "    <PaymentType>";
    xmlReq += "      <Merchant>";
    xmlReq += "        <MerchantID>999999999997</MerchantID>";
    xmlReq += "        <MerchantKey>K3QD6YWyhfD</MerchantKey>";
    xmlReq += "      </Merchant>";
    xmlReq += "      <TransactionBase>";
    xmlReq += "        <TransactionID>";
    xmlReq +=           bstrUti;
    xmlReq += "        </TransactionID>";
    xmlReq += "        <TransactionType>11</TransactionType>";
    xmlReq += "        <Reference1>PO 123456</Reference1>";
    xmlReq += "        <Amount>19.99</Amount>";
    xmlReq += "      </TransactionBase>";
    xmlReq += "      <Customer>";
    xmlReq += "        <Name>";
    xmlReq += "          <FirstName>John</FirstName>";
    xmlReq += "          <MI>A</MI>";
    xmlReq += "          <LastName>Doe</LastName>";
    xmlReq += "        </Name>";
    xmlReq += "        <Address>";
    xmlReq += "          <AddressLine1>12345 Street</AddressLine1>";
    xmlReq += "          <AddressLine2>Apt #2</AddressLine2>";
    xmlReq += "          <City>Some City</City>";
    xmlReq += "          <State>Some State</State>";
    xmlReq += "          <ZipCode>12345</ZipCode>";
    xmlReq += "          <Country>Some Country</Country>";
    xmlReq += "          <EmailAddress>john.doe@domain.com</EmailAddress>";
    xmlReq += "          <Telephone>1234567891</Telephone>";
    xmlReq += "          <Fax>1234567890</Fax>";
    xmlReq += "        </Address>";
    xmlReq += "        <Company>";
    xmlReq += "          <Name>John's Company</Name>";
    xmlReq += "          <Address>";
    xmlReq += "            <AddressLine1>12345 Street</AddressLine1>";
    xmlReq += "            <AddressLine2>Apt #2</AddressLine2>";
    xmlReq += "            <City>Some City</City>";
    xmlReq += "            <State>Some State</State>";
    xmlReq += "            <ZipCode>12345</ZipCode>";
    xmlReq += "            <Country>Some Country</Country>";
    xmlReq += "            <EmailAddress>john.doe@domain.com</EmailAddress>";
    xmlReq += "            <Telephone>1234567891</Telephone>";
    xmlReq += "            <Fax>1234567890</Fax>";
    xmlReq += "          </Address>";
    xmlReq += "        </Company>";
    xmlReq += "      </Customer>";
    xmlReq += "      <ShippingRecipient>";
    xmlReq += "        <Name>";
    xmlReq += "          <FirstName>John</FirstName>";
    xmlReq += "          <MI>A</MI>";
    xmlReq += "          <LastName>Doe</LastName>";
    xmlReq += "        </Name>";
    xmlReq += "        <Address>";
    xmlReq += "          <AddressLine1>12345 Street</AddressLine1>";
    xmlReq += "          <AddressLine2>Apt #2</AddressLine2>";
    xmlReq += "          <City>Some City</City>";
    xmlReq += "          <State>Some State</State>";
    xmlReq += "          <ZipCode>12345</ZipCode>";
    xmlReq += "          <Country>Some Country</Country>";
    xmlReq += "          <EmailAddress>john.doe@domain.com</EmailAddress>";
    xmlReq += "          <Telephone>1234567891</Telephone>";
    xmlReq += "          <Fax>1234567890</Fax>";
    xmlReq += "        </Address>";
    xmlReq += "        <Company>";
    xmlReq += "          <Name>John's Company</Name>";
    xmlReq += "          <Address>";
    xmlReq += "            <AddressLine1>12345 Street</AddressLine1>";
    xmlReq += "            <AddressLine2>Apt #2</AddressLine2>";
    xmlReq += "            <City>Some City</City>";
    xmlReq += "            <State>Some State</State>";
    xmlReq += "            <ZipCode>12345</ZipCode>";
    xmlReq += "            <Country>Some Country</Country>";
    xmlReq += "            <EmailAddress>john.doe@domain.com</EmailAddress>";
    xmlReq += "            <Telephone>1234567891</Telephone>";
    xmlReq += "            <Fax>1234567890</Fax>";
    xmlReq += "          </Address>";
    xmlReq += "        </Company>";
    xmlReq += "      </ShippingRecipient>";
    xmlReq += "    </PaymentType>";
    xmlReq += "  </Payments>";
    xmlReq += "</Request_v1>";

    if( bstrUti )
    	::SysFreeString(bstrUti);

    hr = pMC->GetResponse(xmlReq, &pMR.GetInterfacePtr());
    if( FAILED(hr) )
    {
    	// handle COM error
    }

    hr = pMR->GetStatusCode(&statusCode);
    if( FAILED(hr) )
    {
    	// handle COM error
    }

    if( statusCode != 0 )
    {
    	// a non zero value means the Payment Module encountered an error processing requested data
    	// a transaction status query shoudlbe use to determine the transaction status
    }

    hr = pMR->GetResponseText(&bstrXmlResp);
    if( FAILED(hr) )
    {
    	// handle COM error
    }
    if( bstrXmlResp )
    	::SysFreeString(bstrXmlResp);

    pMC = NULL;
    pMR = NULL;
    ::CoUninitialize();
}

```

**Note:** XML documents should be created using a proper DOM to observe proper XML encoding.

## <a name="ModuleClient"></a> ModuleClient Members

The following tables show the methods.

### Methods

| Method               | Description                                                    |
|----------------------|----------------------------------------------------------------|
| GetUniqueId          | Returns a Globally Unique Identifier                           |
| GetUniqueIds         | Returns an array of Globally Unique Identifiers                |
| GetResponse          | Sends data to the Payment Module and returns the response data |
| SetInstallPromptFlag | Use this method to suppress the SED install prompt             |

### GetUniqueId

Gets a Globally Unique Identifier GUID to be assigned to a transaction for use later in transaction status queries.

#### C# Syntax
```csharp
string GetUniqueId();
```
### GetUniqueIds

Gets an array of Globally Unique Identifiers GUIDs to be assigned to transactions for use later in transaction status queries.

#### C# Syntax
```csharp
string [] GetUniqueIds(int numberOfIds);
```

### GetResponse

Sends XML encoded string data to the Payment Module for processing and returns a Payment Module Response object. This method will block until completion, timeout, or error.

#### C++ Syntax
```cpp
ModuleResponse GetResponse(string request);
```

### SetInstallPromptFlag

Accepts a boolean value. Suppresses the SED install screen if the boolean is false.

#### C++ Syntax
```cpp
HRESULT SetInstallPromptFlag(bool flag);
```

## <a name="ModuleResponse"></a> ModuleResponse Members

The following tables show the methods.

### Methods

| Method               | Description                                            |
|----------------------|--------------------------------------------------------|
| GetStatusCode        | Returns the communication status code                |
| GetStatusDescription | Returns the communication status description          |
| GetResponseText      | Returns the XML encoded response string               |
| GetResponseLength    | Returns the length of the XML encoded response string |

### GetStatusCode

Returns the communication status code. A value of zero means success. A non-zero value indicates an error was encountered communicating with the Payment Module.

#### C# Syntax
```csharp
int GetStatusCode();
```

### GetStatusDescription

Returns the communication error description.

#### C# Syntax
```csharp
string GetStatusDescription();
```

### GetResponseText
Returns the XML encoded response string data from the Payment Module.

#### C# Syntax
```csharp
string GetResponseText();
```

### GetResponseLength

Returns the string length of the XML encoded response.

#### C# Syntax
```csharp
int GetResponseLength();
```

## <a name="Csharp"></a> C# Sample
```csharp

using clSedApi;

void Process()
{
  string xmlResp = null;
  long retVal = 0;
  ModuleClient mc = new ModuleClient();
  mc.SetInstallPromptFlag(false);
  IModuleResponse mr = null;
  string xmlReq = ””;

  string uti = mc.GetUniqueId();

  xmlReq = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
  xmlReq += "<Request_v1 xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">";
  xmlReq += "  <Application>";
  xmlReq += "    <ApplicationID>DEMO</ApplicationID>";
  xmlReq += "    <LanguageID>EN</LanguageID>";
  xmlReq += "  </Application>";
  xmlReq += "  <Payments>";
  xmlReq += "    <PaymentType>";
  xmlReq += "      <Merchant>";
  xmlReq += "        <MerchantID>999999999997</MerchantID>";
  xmlReq += "        <MerchantKey>K3QD6YWyhfD</MerchantKey>";
  xmlReq += "      </Merchant>";
  xmlReq += "      <TransactionBase>";
  xmlReq += "        <TransactionID>";
  xmlReq +=            uti;
  xmlReq += "        </TransactionID>";
  xmlReq += "        <TransactionType>11</TransactionType>";
  xmlReq += "        <Reference1>PO 123456</Reference1>";
  xmlReq += "        <Amount>19.99</Amount>";
  xmlReq += "      </TransactionBase>";
  xmlReq += "      <Customer>";
  xmlReq += "        <Name>";
  xmlReq += "          <FirstName>John</FirstName>";
  xmlReq += "          <MI>A</MI>";
  xmlReq += "          <LastName>Doe</LastName>";
  xmlReq += "        </Name>";
  xmlReq += "        <Address>";
  xmlReq += "          <AddressLine1>12345 Street</AddressLine1>";
  xmlReq += "          <AddressLine2>Apt #2</AddressLine2>";
  xmlReq += "          <City>Some City</City>";
  xmlReq += "          <State>Some State</State>";
  xmlReq += "          <ZipCode>12345</ZipCode>";
  xmlReq += "          <Country>Some Country</Country>";
  xmlReq += "          <EmailAddress>john.doe@domain.com</EmailAddress>";
  xmlReq += "          <Telephone>1234567891</Telephone>";
  xmlReq += "          <Fax>1234567890</Fax>";
  xmlReq += "        </Address>";
  xmlReq += "        <Company>";
  xmlReq += "          <Name>John's Company</Name>";
  xmlReq += "          <Address>";
  xmlReq += "            <AddressLine1>12345 Street</AddressLine1>";
  xmlReq += "            <AddressLine2>Apt #2</AddressLine2>";
  xmlReq += "            <City>Some City</City>";
  xmlReq += "            <State>Some State</State>";
  xmlReq += "            <ZipCode>12345</ZipCode>";
  xmlReq += "            <Country>Some Country</Country>";
  xmlReq += "            <EmailAddress>john.doe@domain.com</EmailAddress>";
  xmlReq += "            <Telephone>1234567891</Telephone>";
  xmlReq += "            <Fax>1234567890</Fax>";
  xmlReq += "          </Address>";
  xmlReq += "        </Company>";
  xmlReq += "      </Customer>";
  xmlReq += "      <ShippingRecipient>";
  xmlReq += "        <Name>";
  xmlReq += "          <FirstName>John</FirstName>";
  xmlReq += "          <MI>A</MI>";
  xmlReq += "          <LastName>Doe</LastName>";
  xmlReq += "        </Name>";
  xmlReq += "        <Address>";
  xmlReq += "          <AddressLine1>12345 Street</AddressLine1>";
  xmlReq += "          <AddressLine2>Apt #2</AddressLine2>";
  xmlReq += "          <City>Some City</City>";
  xmlReq += "          <State>Some State</State>";
  xmlReq += "          <ZipCode>12345</ZipCode>";
  xmlReq += "          <Country>Some Country</Country>";
  xmlReq += "          <EmailAddress>john.doe@domain.com</EmailAddress>";
  xmlReq += "          <Telephone>1234567891</Telephone>";
  xmlReq += "          <Fax>1234567890</Fax>";
  xmlReq += "        </Address>";
  xmlReq += "        <Company>";
  xmlReq += "          <Name>John's Company</Name>";
  xmlReq += "          <Address>";
  xmlReq += "            <AddressLine1>12345 Street</AddressLine1>";
  xmlReq += "            <AddressLine2>Apt #2</AddressLine2>";
  xmlReq += "            <City>Some City</City>";
  xmlReq += "            <State>Some State</State>";
  xmlReq += "            <ZipCode>12345</ZipCode>";
  xmlReq += "            <Country>Some Country</Country>";
  xmlReq += "            <EmailAddress>john.doe@domain.com</EmailAddress>";
  xmlReq += "            <Telephone>1234567891</Telephone>";
  xmlReq += "            <Fax>1234567890</Fax>";
  xmlReq += "          </Address>";
  xmlReq += "        </Company>";
  xmlReq += "      </ShippingRecipient>";
  xmlReq += "    </PaymentType>";
  xmlReq += "  </Payments>";
  xmlReq += "</Request_v1>";

  mr = mc.GetResponse(xmlReq);

  retVal = mr.GetStatusCode();
  if( retVal != 0 )
  {
    // handle Payment Module communication error
    // a transaction status query should be used to determine the status of the transaction
  }

  xmlResp = mr.GetResponseText();
}
```

**Note:** XML documents should be created using a proper DOM to observe proper XML encoding.

## <a name="VB6"></a> VB 6 Sample

```vb
sub Process()
Dim xmlResp as string
Dim retVal as integer
Dim mc as Object
Dim mr as Object
Dim xmlReq as string
Dim uti as string

Mc = CreateObject(“clSedApi.ModuleClient”)
mc.SetInstallPromptFlag(false)
uti = mc.GetUniqueId()

xmlReq = "<?xml version=\"1.0\" encoding=\"utf-16\"?>"
xmlReq = xmlReq & "<Request_v1 xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">"
xmlReq = xmlReq & "  <Application>"
xmlReq = xmlReq & "    <ApplicationID>DEMO</ApplicationID>"
xmlReq = xmlReq & "    <LanguageID>EN</LanguageID>"
xmlReq = xmlReq & "  </Application>"
xmlReq = xmlReq & "  <Payments>"
xmlReq = xmlReq & "    <PaymentType>"
xmlReq = xmlReq & "      <Merchant>"
xmlReq = xmlReq & "        <MerchantID>999999999997</MerchantID>"
xmlReq = xmlReq & "        <MerchantKey>K3QD6YWyhfD</MerchantKey>"
xmlReq = xmlReq & "      </Merchant>"
xmlReq = xmlReq & "      <TransactionBase>"
xmlReq = xmlReq & "        <TransactionID>"
xmlReq = xmlReq &           uti
xmlReq = xmlReq & "        </TransactionID>"
xmlReq = xmlReq & "        <TransactionType>11</TransactionType>"
xmlReq = xmlReq & "        <Reference1>PO 123456</Reference1>"
xmlReq = xmlReq & "        <Amount>19.99</Amount>"
xmlReq = xmlReq & "      </TransactionBase>"
xmlReq = xmlReq & "      <Customer>"
xmlReq = xmlReq & "        <Name>"
xmlReq = xmlReq & "          <FirstName>John</FirstName>"
xmlReq = xmlReq & "          <MI>A</MI>"
xmlReq = xmlReq & "          <LastName>Doe</LastName>"
xmlReq = xmlReq & "        </Name>"
xmlReq = xmlReq & "        <Address>"
xmlReq = xmlReq & "          <AddressLine1>12345 Street</AddressLine1>"
xmlReq = xmlReq & "          <AddressLine2>Apt #2</AddressLine2>"
xmlReq = xmlReq & "          <City>Some City</City>"
xmlReq = xmlReq & "          <State>Some State</State>"
xmlReq = xmlReq & "          <ZipCode>12345</ZipCode>"
xmlReq = xmlReq & "          <Country>Some Country</Country>"
xmlReq = xmlReq & "          <EmailAddress>john.doe@domain.com</EmailAddress>"
xmlReq = xmlReq & "          <Telephone>1234567891</Telephone>"
xmlReq = xmlReq & "          <Fax>1234567890</Fax>"
xmlReq = xmlReq & "        </Address>"
xmlReq = xmlReq & "        <Company>"
xmlReq = xmlReq & "          <Name>John's Company</Name>"
xmlReq = xmlReq & "          <Address>"
xmlReq = xmlReq & "            <AddressLine1>12345 Street</AddressLine1>"
xmlReq = xmlReq & "            <AddressLine2>Apt #2</AddressLine2>"
xmlReq = xmlReq & "            <City>Some City</City>"
xmlReq = xmlReq & "            <State>Some State</State>"
xmlReq = xmlReq & "            <ZipCode>12345</ZipCode>"
xmlReq = xmlReq & "            <Country>Some Country</Country>"
xmlReq = xmlReq & "            <EmailAddress>john.doe@domain.com</EmailAddress>"
xmlReq = xmlReq & "            <Telephone>1234567891</Telephone>"
xmlReq = xmlReq & "            <Fax>1234567890</Fax>"
xmlReq = xmlReq & "          </Address>"
xmlReq = xmlReq & "        </Company>"
xmlReq = xmlReq & "      </Customer>"
xmlReq = xmlReq & "      <ShippingRecipient>"
xmlReq = xmlReq & "        <Name>"
xmlReq = xmlReq & "          <FirstName>John</FirstName>"
xmlReq = xmlReq & "          <MI>A</MI>"
xmlReq = xmlReq & "          <LastName>Doe</LastName>"
xmlReq = xmlReq & "        </Name>"
xmlReq = xmlReq & "        <Address>"
xmlReq = xmlReq & "          <AddressLine1>12345 Street</AddressLine1>"
xmlReq = xmlReq & "          <AddressLine2>Apt #2</AddressLine2>"
xmlReq = xmlReq & "          <City>Some City</City>"
xmlReq = xmlReq & "          <State>Some State</State>"
xmlReq = xmlReq & "          <ZipCode>12345</ZipCode>"
xmlReq = xmlReq & "          <Country>Some Country</Country>"
xmlReq = xmlReq & "          <EmailAddress>john.doe@domain.com</EmailAddress>"
xmlReq = xmlReq & "          <Telephone>1234567891</Telephone>"
xmlReq = xmlReq & "          <Fax>1234567890</Fax>"
xmlReq = xmlReq & "        </Address>"
xmlReq = xmlReq & "        <Company>"
xmlReq = xmlReq & "          <Name>John's Company</Name>"
xmlReq = xmlReq & "          <Address>"
xmlReq = xmlReq & "            <AddressLine1>12345 Street</AddressLine1>"
xmlReq = xmlReq & "            <AddressLine2>Apt #2</AddressLine2>"
xmlReq = xmlReq & "            <City>Some City</City>"
xmlReq = xmlReq & "            <State>Some State</State>"
xmlReq = xmlReq & "            <ZipCode>12345</ZipCode>"
xmlReq = xmlReq & "            <Country>Some Country</Country>"
xmlReq = xmlReq & "            <EmailAddress>john.doe@domain.com</EmailAddress>"
xmlReq = xmlReq & "            <Telephone>1234567891</Telephone>"
xmlReq = xmlReq & "            <Fax>1234567890</Fax>"
xmlReq = xmlReq & "          </Address>"
xmlReq = xmlReq & "        </Company>"
xmlReq = xmlReq & "      </ShippingRecipient>"
xmlReq = xmlReq & "    </PaymentType>"
xmlReq = xmlReq & "  </Payments>"
xmlReq = xmlReq & "</Request_v1>"

mr = mc.GetResponse(xmlReq)

retVal = mr.GetStatusCode()
If retVal <> 0 Then

‘ handle Payment Module communication error
‘ a transaction status query should be used to determine the status of the transaction
End If

xmlResp = mr.GetResponseText()
end sub
```

**Note:** XML documents should be created using a proper DOM to observe proper XML encoding.

## <a name="FlowDiagram"></a> Sage Exchange Desktop Payment Process Flow
![SED Payment Process Diagram](./Images/SEDPaymentFlowDiagram.jpg)
