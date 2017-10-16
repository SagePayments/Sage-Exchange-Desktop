## Message formats
The following illustrations provide examples of how the request message components are structured. All requests are framed within a Request_v1 root and must include an Application element. Optional XML elements can be omitted from the request message when not used. Some elements are arrays. In cases where more than one element is present in the array, processing will occur in FIFO. XML reserved characters must be observed and encoded appropriately.

This document provides sample XML messages for the following transaction types:
1. [Sale](# Sale)
1. [Authorization](#Authorization)
1. [Capture](#Capture)
1. [Force](#Force)
1. [Level 2](#Level2)
1. [Void](#Void)
1. [Credit](#Credit)
1. [Single Payment Response](#SingleResponse)
1. [Batch Inquiry](#BatchInquiry)
1. [Batch Close](#BatchClose)
1. [VaultOperation](#VaultOperation)
1. [Transaction Status Query](#TransQuery)
1. [Vault Status Query](#VaultQuery)
1. [Multi-payment Processing](#MultiPayment)
1. [Terminal Item List](#TerminalItemList)
1. [Healthcare Signature](#HealthcareSig)
1. [Tender Type](#TenderType)
1. [Terminal Debit Configuration](#TerminalDebitConfig)
1. [Healthcare](#Healthcare)
1. [EMV Sale](#EMVSale)


### <a name="Sale"></a> Sale
Sale is a transaction type in which the transaction is authorized and captured in one step. Sale is used only to process purchases of goods or services that do not require physical shipment or "soft" goods that are delivered electronically.

#### XML sale request with user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
  </Payments>
</Request_v1>
```

#### XML sale request without user interface sample

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>e856a127-8527-431c-807f-6efacd8bdf83</TransactionID>
        <TransactionType>01</TransactionType>
        <Reference1>INV# 451777674</Reference1>
        <Amount>2152.92</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>John</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>12345 Street</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>john.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
      <VaultStorage>
        <GUID>dd83d7559a274fb2b66e774a4febced7</GUID>
        <Service>RETRIEVE</Service>
      </VaultStorage>
    </PaymentType>
  </Payments>
</Request_v1>
```

#### XML sale request and vault storage with user interface sample

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>e856a127-8527-431c-807f-6efacd8bdf83</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 451777674</Reference1>
        <Amount>2152.92</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>John</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>12345 Street</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>john.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
      <VaultStorage>
        <Service>CREATE</Service>
      </VaultStorage>
    </PaymentType>
  </Payments>
</Request_v1>
```
### <a name="Authorization"></a> Authorization
Authorization is a transaction type in which an account is verified to be valid and has not reached its limit. The total amount of the transaction is reserved against the account balance. Authorizations are used if goods are to be physically shipped or in other cases for which the merchant must first verify whether the order can be fulfilled. An approved Authorization is followed by a Capture, which prepares it for settlement.
#### XML authorization request with user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>12</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
  </Payments>
</Request_v1>
```

#### XML authorization request without user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>e856a127-8527-431c-807f-6efacd8bdf83</TransactionID>
        <TransactionType>02</TransactionType>
        <Reference1>INV# 451777674</Reference1>
        <Amount>2152.92</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>John</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>12345 Street</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>john.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
      <VaultStorage>
        <GUID>dd83d7559a274fb2b66e774a4febced7</GUID>
        <Service>RETRIEVE</Service>
      </VaultStorage>
    </PaymentType>
  </Payments>
</Request_v1>
```
### <a name="Capture" ></a> Capture
Capture is a transaction type that puts an Authorization transaction into a Captured state for settlement. In the case of partial shipments, the Capture amount may be less than the Authorization amount. Captures can be initiated only after the purchased goods have been shipped.
#### XML capture request with user interface sample
  ```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>0405aa29-9be2-4c46-b8b0-b103e25a39b6</TransactionID>
        <TransactionType>13</TransactionType>
        <Amount>4577.52</Amount>
        <VANReference>CBK9A0j650</VANReference>
      </TransactionBase>
    </PaymentType>
  </Payments>
</Request_v1>
```

#### XML capture request without user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>0405aa29-9be2-4c46-b8b0-b103e25a39b6</TransactionID>
        <TransactionType>03</TransactionType>
        <Amount>4577.52</Amount>
        <VANReference>CBK9A0j650</VANReference>
      </TransactionBase>
    </PaymentType>
  </Payments>
</Request_v1>
```

### <a name="Force"></a> Force
Force is a transaction type used to force a transaction into settlement in cases where a Sale or Authorization transaction cannot be processed and the Authorization Code is obtained from an outside source.
#### XML force request with user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>15</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
        <AuthCode>123456</AuthCode>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
  </Payments>
</Request_v1>
```

#### XML force request without user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>e856a127-8527-431c-807f-6efacd8bdf83</TransactionID>
        <TransactionType>05</TransactionType>
        <Reference1>INV# 451777674</Reference1>
        <Amount>2152.92</Amount>
        <AuthCode>123456</AuthCode>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>John</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>12345 Street</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>john.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
      <VaultStorage>
        <GUID>dd83d7559a274fb2b66e774a4febced7</GUID>
        <Service>RETRIEVE</Service>
      </VaultStorage>
    </PaymentType>
  </Payments>
</Request_v1>
```

### <a name="Level2"></a> Level 2
Level 2 data is additional transaction data required for Level II commercial card qualification for credit card transactions.

#### XML Level 2 sale request with user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
      <Level2>
        <CustomerNumber>123456789012</CustomerNumber>
        <TaxAmount>92.59</TaxAmount>
      </Level2>
    </PaymentType>
  </Payments>
</Request_v1>
```

### <a name="Void"></a> Void
Void is a transaction type that cancels a transaction that has not yet been settled.

#### XML void request without user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>782ad8d0-0dd2-4763-8d64-cc9fddfad441</TransactionID>
        <TransactionType>04</TransactionType>
        <VANReference>ABL9LKQaI0</VANReference>
      </TransactionBase>
    </PaymentType>
  </Payments>
</Request_v1>
```

### <a name="Credit"></a> Credit
Credit is a transaction type that transfers funds to the account, rather than from the account. This transaction type is typically used to refund money for a transaction that was previously settled.

#### XML credit request with user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>0405aa29-9be2-4c46-b8b0-b103e25a39b6</TransactionID>
        <TransactionType>16</TransactionType>
        <Amount>4577.52</Amount>
        <VANReference>CBK9A0j650</VANReference>
      </TransactionBase>
    </PaymentType>
  </Payments>
</Request_v1>
```

#### XML credit request without user interface sample
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>0405aa29-9be2-4c46-b8b0-b103e25a39b6</TransactionID>
        <TransactionType>06</TransactionType>
        <Amount>4577.52</Amount>
        <VANReference>CBK9A0j650</VANReference>
      </TransactionBase>
    </PaymentType>
  </Payments>
</Request_v1>
```

### <a name="SingleResponse"></a> Single Payment Response
Below is a typical response for a single payment.

#### XML payment response

```xml
<?xml version="1.0" encoding="utf-16"?>
<Response_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <PaymentResponses>
    <PaymentResponseType>
      <Response>
        <ResponseIndicator>A</ResponseIndicator>
        <ResponseCode>024702</ResponseCode>
        <ResponseMessage>APPROVED</ResponseMessage>
      </Response>
      <TransactionResponse>
        <AuthCode>024702</AuthCode>
        <AVSResult />
        <CVVResult>N</CVVResult>
        <VANReference>GAUD9m1Bj0</VANReference>
        <TransactionID>70d0d88f-bc2d-4c61-a6ad-843dab2cb649</TransactionID>
        <Last4>XXXXXXXXXXXX1111</Last4>
        <PaymentDescription>411111XXXXXX1111</PaymentDescription>
        <Amount>19.99</Amount>
        <PaymentTypeID>4</PaymentTypeID>
        <Reference1>PO 123456</Reference1>
        <TransactionDate>10/30/2013 9:47:00 AM</TransactionDate>
        <EntryMode>K</EntryMode>
        <TaxAmount>0</TaxAmount>
        <ShippingAmount>0</ShippingAmount>
        <TransactionPaymentType>CREDITCARD</TransactionPaymentType>
        <CashbackAmount>0</CashbackAmount>
        <FSACard>false</FSACard>
      </TransactionResponse>
      <Customer>
        <Name>
          <FirstName>John A Doe</FirstName>
          <MI />
          <LastName />
        </Name>
        <Address>
          <AddressLine1>12345 Street</AddressLine1>
          <AddressLine2>Apt #2</AddressLine2>
          <City>Some City</City>
          <State>Alabama</State>
          <ZipCode>12345</ZipCode>
          <Country>United States</Country>
          <EmailAddress>john.doe@domain.com</EmailAddress>
          <Telephone>1234567891</Telephone>
          <Fax>1234567890</Fax>
        </Address>
        <Company>
          <Name>John's Company</Name>
          <Address>
            <AddressLine1>12345 Street</AddressLine1>
            <AddressLine2>Apt #2</AddressLine2>
            <City>Some City</City>
            <State>Some State</State>
            <ZipCode>12345</ZipCode>
            <Country>Some Country</Country>
            <EmailAddress>john.doe@domain.com</EmailAddress>
            <Telephone>1234567891</Telephone>
            <Fax>1234567890</Fax>
          </Address>
        </Company>
      </Customer>
      <ShippingRecipient>
        <Name>
          <FirstName>John</FirstName>
          <MI>A</MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>12345 Street</AddressLine1>
          <AddressLine2>Apt #2</AddressLine2>
          <City>Some City</City>
          <State>Some State</State>
          <ZipCode>12345</ZipCode>
          <Country>Some Country</Country>
          <EmailAddress>john.doe@domain.com</EmailAddress>
          <Telephone>1234567891</Telephone>
          <Fax>1234567890</Fax>
        </Address>
        <Company>
          <Name>John's Company</Name>
          <Address>
            <AddressLine1>12345 Street</AddressLine1>
            <AddressLine2>Apt #2</AddressLine2>
            <City>Some City</City>
            <State>Some State</State>
            <ZipCode>12345</ZipCode>
            <Country>Some Country</Country>
            <EmailAddress>john.doe@domain.com</EmailAddress>
            <Telephone>1234567891</Telephone>
            <Fax>1234567890</Fax>
          </Address>
        </Company>
      </ShippingRecipient>
    </PaymentResponseType>
  </PaymentResponses>
</Response_v1>
```

### <a name="BatchInquiry"></a> Batch Inquiry
Batch inquiry is used to obtain the transaction net and transaction count of the current open batch awaiting settlement.

#### XML batch inquiry request

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Batch>
    <Merchant>
      <MerchantID>999999999997</MerchantID>
      <MerchantKey>K3QD6YWyhfD</MerchantKey>
    </Merchant>
    <Net>-1</Net>
    <Count>-1</Count>
    <BatchPayment>CREDITCARD</BatchPayment>
  </Batch>
</Request_v1>
```

### <a name="BatchClose"></a> Batch Close
Batch Close is used to settle transactions in the current open batch awaiting settlement.
Sales, Captures, Forces, and Credit transactions qualify for settlement.

#### XML batch close request with net and count verification

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Batch>
    <Merchant>
      <MerchantID>999999999997</MerchantID>
      <MerchantKey>K3QD6YWyhfD</MerchantKey>
    </Merchant>
    <Net>2561.23</Net>
    <Count>5</Count>
    <BatchPayment>CREDITCARD</BatchPayment>
  </Batch>
</Request_v1>
```

#### XML batch close request without net and count verification

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Batch>
    <Merchant>
      <MerchantID>999999999997</MerchantID>
      <MerchantKey>K3QD6YWyhfD</MerchantKey>
    </Merchant>
    <Net>0</Net>
    <Count>0</Count>
    <BatchPayment>CREDITCARD</BatchPayment>
  </Batch>
</Request_v1>
```

### <a name="VaultOperation"></a> Vault Operation
Vault Operation is used to capture sensitive card holder data and insert or update the storage in the Sage Payment Solutions Vault. No payment is processed.

#### XML vault operation request for creating a storage record

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <VaultOperation>
    <Merchant>
      <MerchantID>999999999997</MerchantID>
      <MerchantKey>K3QD6YWyhfD</MerchantKey>
    </Merchant>
    <VaultStorage>
      <Service>CREATE</Service>
    </VaultStorage>
    <VaultID>2341234-12431243-2341235</VaultID>
  </VaultOperation>
</Request_v1>
```

#### XML vault operation request for updating a storage record

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <VaultOperation>
    <Merchant>
      <MerchantID>999999999997</MerchantID>
      <MerchantKey>K3QD6YWyhfD</MerchantKey>
    </Merchant>
    <VaultStorage>
      <Service>UPDATE</Service>
      <GUID>sfdas-ee3u38d-dagdi3-efad83</GUID>
    </VaultStorage>
    <VaultID>2341234-12431243-2341235</VaultID>
  </VaultOperation>
</Request_v1>
```

### <a name="TransQuery"></a> Transaction Status Query
Transaction Status Query is used to get the status of a transaction processed through the
Sage Exchange by using the user defined Transaction ID provided during a previous Payment. In the event of a communication failure in which the response was not received this can be used to determine if the platform received and processed the transaction or if the transaction needs to be run again. This can be used to prevent duplicated transactions. If post transaction logic is required this can also provide additional information such as the current state of the transaction (settled/expired/voided).

#### XML transaction status query

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <TransactionStatusQueries>
    <TransactionStatusQueryType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionID>sdfasdf089273412903479a87sa</TransactionID>
    </TransactionStatusQueryType>
  </TransactionStatusQueries>
</Request_v1>
```

### <a name="VaultQuery"></a> Vault Status Query
Vault Status Query is used to get the status of a Vault Operation processed through the Sage Exchange by using the user defined Vault ID provided during a previous Vault Operation. In the event of a communication failure in which the response was not received this can be used to determine if the platform received and processed the operation or if the operation needs to be run again.

#### XML vault status query

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <VaultStatusQuery>
    <Merchant>
      <MerchantID>999999999997</MerchantID>
      <MerchantKey>K3QD6YWyhfD</MerchantKey>
    </Merchant>
    <VaultID>sdfasdf089273412903479a87sa</VaultID>
  </VaultStatusQuery>
</Request_v1>
```

### <a name="MultiPayment"></a> Multi-payment Processing
The Sage Exchange application supports processing multiple payments in a single XML request. The request can combine both UI and Non-UI payments. If any payment contains a UI transaction type then the Multi-Payment UI will be shown.

#### XML multi-payment request with user interface

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>4fa9747c-13a2-46af-970f-f8a92f5d4f61</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 7563456</Reference1>
        <Amount>50.50</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>John</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>4567 Street</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>john.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
  </Payments>
</Request_v1>
```

### <a name="TerminalItemList"></a> Terminal Item List
The Sage Exchange application supports hardware terminals that can display a listing of items being purchased. The follow request can be used to display a list of items on these support terminals.

#### XML terminal items list request

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <TerminalItemListDisplay>
    <TerminalDisplayItems>
      <TerminalDisplayItemType>
        <Quantity>5</Quantity>
        <Description>Item 1</Description>
        <Price>1.00</Price>
      </TerminalDisplayItemType>
      <TerminalDisplayItemType>
        <Quantity>10</Quantity>
        <Description>Item 2</Description>
        <Price>2.12</Price>
      </TerminalDisplayItemType>
    </TerminalDisplayItems>
  </TerminalItemListDisplay>
</Request_v1>
```

### <a name="HealthcareSig"></a> Healthcare Signature
The Sage Exchange application supports hardware terminals that can capture an image of a signature. Signatures that are captured are linked to a previously process transaction via the VANReference.

#### XML healthcare signature request

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <HealthcareSignature>
    <Merchant>
      <MerchantID>999999999997</MerchantID>
      <MerchantKey>K3QD6YWyhfD</MerchantKey>
    </Merchant>
    <VANReference>A123456789</VANReference>
    <SignatureAttributes>
      <SignatureAttributeType>
        <Name>COUNSELING</Name>
        <DisplayText>I was offered couseling</DisplayText>
      </SignatureAttributeType>
      <SignatureAttributeType>
        <Name>NARCOTICS</Name>
        <DisplayText>I acknowledge receipt of the listed prescriptions</DisplayText>
      </SignatureAttributeType>
      <SignatureAttributeType>
        <Name>EZCAP</Name>
        <DisplayText>I acknowledge Ez Open Caps</DisplayText>
      </SignatureAttributeType>
      <SignatureAttributeType>
        <Name>PRIVACY</Name>
        <DisplayText>I acknowledge the Notice of Privacy Practices</DisplayText>
      </SignatureAttributeType>
    </SignatureAttributes>
    <ItemsDisplayText>I acknowledge receipt of the listed prescriptions</ItemsDisplayText>
    <Items>
      <string>RX12345</string>
      <string>RX345</string>
    </Items>
  </HealthcareSignature>
</Request_v1>
```

#### XML healthcare signature response

```xml
<?xml version="1.0" encoding="utf-16"?>
<Response_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <HealthcareSignatureResponse>
    <Response>
      <ResponseIndicator>A</ResponseIndicator>
      <ResponseMessage>ACCEPTED</ResponseMessage>
    </Response>
    <ImageData>iVBORw0KGgoAAAANSUhEUgAAAIYAAAB/AQMAAAAQKigDAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAGUExUR QAAAP///6XZn90AAAAJcEhZcwAADsMAAA7DAcdvqGQAAAFwSURBVEjH1dQ7UsQwDAZgbrBH4ljbwPgoewQKasZHyAnA5Xa42BkEKLbwM
      7Elh812EBeZ+RrZ8m/dEfvmfyLzgQsCFxBid4gRoi0XxcUTFzcQwwR3COwQu0OMEB3W7aLCull8WNfEDcRdFRwIrmCjwECaSzY7REex8 UphU1QWs4insZhGXJHQ5nNuQCdzFNwQHdtcBLYkNvVM3+noQkwS1YjO4pJ8jEVF8bSKpyyY5D0dggsWgV8Eitgkb2nLUVwjppPXRswiu hFLL2nLWXQVn9OCq7gqakMe05aZQBFPVawQkwVSUq0PP93KNBZVJGV3cuV5LPLkkL6K5CEWBXpBpKmTYxBdJQ2oI6BXrXgEzG8qiKlyW SSexjn7eegE3SkHOkksAb4OgSrrQByLypfJRAtRrcRMNIOjiBOCncQyIMQKMUK0EMVlJi4oBIRYIUaI7sV2xVM2uuI5qxcmLmeiEU/3T
      OiBuDwLmYTQXxaafwC3lDfXH8bD9gAAAABJRU5ErkJggg==</ImageData>
    </HealthcareSignatureResponse>
  </Response_v1>
```

### <a name="TenderType"></a> Tender Type
It is possible to explicitly request the tender type to be used for a given transaction.  This is used when the calling application has already determined the method of payment.  This is an example of a Sale transaction requesting credit/debit card processing.

#### XML credit card sale request with user interface sample

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
        <TenderType>CREDITCARD</TenderType>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
  </Payments>
</Request_v1>
```

#### XML cash sale request with user interface sample

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
        <TenderType>CASH</TenderType>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
  </Payments>
</Request_v1>
```

### <a name="TerminalDebitConfig"></a> Terminal Debit Configuration
Sage Exchange supports hardware terminals that can process debit (PIN Entry) and cash back.  By default Sage Exchange will process debit (PIN Entry) on terminals and cards that support it. If cash back is required additional amounts need to be provided. The following request is a Sale with the additional terminal debit configuration.

##### XML Credit Card Sale Request with User Interface and Terminal Debit Configuration Sample

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>1892.59</Amount>
        <TenderType>CREDITCARD</TenderType>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
    </PaymentType>
  </Payments>
  <TerminalDebitConfiguration>
    <AllowDebit>true</AllowDebit>
    <CashbackAmounts>
      <int>10</int>
      <int>20</int>
      <int>30</int>
      <int>40</int>
    </CashbackAmounts>
  </TerminalDebitConfiguration>
</Request_v1>
```

### <a name="Healthcare"></a> Healthcare
Healthcare data is additional transaction data required for healthcare reporting.

#### XML healthcare sale request with user interface sample

```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Application>
    <ApplicationID>DEMO</ApplicationID>
    <LanguageID>EN</LanguageID>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>999999999997</MerchantID>
        <MerchantKey>K3QD6YWyhfD</MerchantKey>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>INV# 886478943</Reference1>
        <Amount>12.50</Amount>
        <TenderType>CREDITCARD</TenderType>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Jane</FirstName>
          <MI> </MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>jane.doe@sagepayments.com</EmailAddress>
          <Telephone></Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
      <Healthcare>
        <HealthcareAmount>12.50</HealthcareAmount>
        <ClinicAmount>0.00</ClinicAmount>
        <DentalAmount>0.00</DentalAmount>
        <VisionAmount>0.00</VisionAmount>
        <PerscriptionAmount>0.00</PerscriptionAmount>
        <IIASVerification>0</IIASVerification>
        <HealthcareItems>
          <HealthcareItemType>
            <Quantity>2</Quantity>
            <Description>My Item</Description>
            <Price>6.25</Price>
          </HealthcareItemType>
        </HealthcareItems>
      </Healthcare>
    </PaymentType>
  </Payments>
</Request_v1>
```
### <a name="EMVSale"></a> EMV Sale
The EMV Sale transaction makes it possible to process sale transactions using chip enabled cards and terminals.

#### EMV Sale Request
```xml
<?xml version="1.0" encoding="utf-16"?>
<Request_v1 xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
  <Application>
    <ApplicationID>DDMSPLUS1000000ERPBABP9USEN</ApplicationID>
    <LanguageID>EN</LanguageID>
    <ClientID>[Sage Provided]</ClientID>
    <ClientKey>[Sage Provided]</ClientKey>
  </Application>
  <Payments>
    <PaymentType>
      <Merchant>
        <MerchantID>173859436515</MerchantID>
        <MerchantKey>P1J2V8P2Q3D8</MerchantKey>
      </Merchant>
      </Merchant>
      <TransactionBase>
        <TransactionID>5ea9747c-12a4-46af-970f-f8a92f6d4f65</TransactionID>
        <TransactionType>11</TransactionType>
        <Reference1>257000</Reference1>
        <Amount>46.04</Amount>
      </TransactionBase>
      <Customer>
        <Name>
          <FirstName>Test</FirstName>
          <MI></MI>
          <LastName>User</LastName>
        </Name>
        <Address>
          <AddressLine1>67890 Road</AddressLine1>
          <AddressLine2></AddressLine2>
          <City>South Padre Island</City>
          <State>Texas</State>
          <ZipCode>78597</ZipCode>
          <Country>USA</Country>
          <EmailAddress>winnie@north49.com</EmailAddress>
          <Telephone>703-555-1234</Telephone>
          <Fax></Fax>
        </Address>
      </Customer>
      <ShippingRecipient>
        <Name>
          <FirstName>John</FirstName>
          <MI>A</MI>
          <LastName>Doe</LastName>
        </Name>
        <Address>
          <AddressLine1>12345 Street</AddressLine1>
          <AddressLine2>Apt #2</AddressLine2>
          <City>Some City</City>
          <State>Some State</State>
          <ZipCode>T6W 1A1</ZipCode>
          <Country>Canada</Country>
          <EmailAddress>john.doe@domain.com</EmailAddress>
          <Telephone>1234567891</Telephone>
          <Fax>1234567890</Fax>
        </Address>
      </ShippingRecipient>
    </PaymentType>
  </Payments>
</Request_v1>
```
#### EMV Sale Response
```xml

Status Code: 0
Status Description: OK
Response Length: 10082

<?xml version="1.0" encoding="utf-16"?>
<Response_v1 xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <PaymentResponses>
    <PaymentResponseType>
      <Response>
        <ResponseIndicator>A</ResponseIndicator>
        <ResponseCode>05157C</ResponseCode>
        <ResponseMessage>APPROVED 05157C</ResponseMessage>
      </Response>
      <TransactionResponse>
        <AuthCode>05157C</AuthCode>
        <AVSResult />
        <CVVResult>P</CVVResult>
        <VANReference>C8VHEk4IZ0</VANReference>
        <TransactionID />
        <Last4>XXXXXXXXXXXX6824</Last4>
        <PaymentDescription>414720XXXXXX6824</PaymentDescription>
        <Amount>0.02</Amount>
        <PaymentTypeID>4</PaymentTypeID>
        <Reference1>Order # </Reference1>
        <TransactionDate>8/31/2017 2:45:05 PM</TransactionDate>
        <EntryMode>I</EntryMode>
        <TaxAmount>0</TaxAmount>
        <ShippingAmount>0</ShippingAmount>
        <TransactionPaymentType>CREDITCARD</TransactionPaymentType>
        <CashbackAmount>0</CashbackAmount>
        <FSACard>false</FSACard>
        <CardExpirationDate>0918</CardExpirationDate>
        <SignatureImageData>AAgEAAQALP4hvqlx4LWAjP/3/b7t/4/7f9AOgHgDwB0A8AdAOgPgDwJ6PUDoB0H34DsByA6A9gPAHYHwD2B7A9gfAPYHwD4C8BeA/A4AQoAIBDACAiQAoIkAKBEACgRAAgQ8MLZySLcwM98egPxAAgNAIgCMADPwLVPg89e+v/Qn4UAD5ax4r0PYA8fgghwBT4oAI/FACPxQAj8UAQ+OADvwr8vg/8eEOynhbsx4Q8IdkvB3wgX0N4G8C+CPA4BIkkSGACeAugPYLoDyC5A8gOgHT/x9489efvAHgLwJ4G+GAFARAAgZIckICGgBvshRwA==</SignatureImageData>
        <SignatureFormat>PNG</SignatureFormat>
      </TransactionResponse>
      <Customer>
        <Name>
          <FirstName>John</FirstName>
          <MI />
          <LastName>Smith</LastName>
        </Name>
        <Address>
          <AddressLine1>123 Address St</AddressLine1>
          <AddressLine2>Apt Z</AddressLine2>
          <City>Cityville</City>
          <State>VA</State>
          <ZipCode>12345</ZipCode>
          <Country>USA</Country>
          <EmailAddress>john.smith@example.com</EmailAddress>
          <Telephone>999-555-1234</Telephone>
          <Fax>999-555-4321</Fax>
        </Address>
        <Company>
          <Address />
        </Company>
      </Customer>
      <ShippingRecipient>
        <Name />
        <Address />
        <Company>
          <Address />
        </Company>
      </ShippingRecipient>
      <EmvResponse>
        <EmvTags>
          <EmvTagType>
            <Tag>95</Tag>
            <Value>0800008000</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>5F34</Tag>
            <Value>02</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>5F20</Tag>
            <Value>NELSON/MICHAEL</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>82</Tag>
            <Value>3C00</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>84</Tag>
            <Value>A0000000031010</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F06</Tag>
            <Value>A0000000031010</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F27</Tag>
            <Value>40</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F26</Tag>
            <Value>5872E3B24E953CB4</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F36</Tag>
            <Value>0063</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F10</Tag>
            <Value>06010A03A02000</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F34</Tag>
            <Value>5E0000</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F37</Tag>
            <Value>D008BF7A</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9B</Tag>
            <Value>E800</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>5F24</Tag>
            <Value>180930</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>5F30</Tag>
            <Value>0201</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F1F</Tag>
            <Value>31313030303034313030383539303030303030</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>4F</Tag>
            <Value />
          </EmvTagType>
          <EmvTagType>
            <Tag>9F11</Tag>
            <Value>01</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F12</Tag>
            <Value>43484153452056495341</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>50</Tag>
            <Value>VISA CREDIT</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F40</Tag>
            <Value>4000F09001</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F02</Tag>
            <Value>000000000002</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F03</Tag>
            <Value>000000000000</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F39</Tag>
            <Value>05</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F33</Tag>
            <Value>E0F8C8</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F1A</Tag>
            <Value>0840</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F35</Tag>
            <Value>22</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>5F2A</Tag>
            <Value>0840</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9A</Tag>
            <Value>170831</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F21</Tag>
            <Value>183802</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9C</Tag>
            <Value>00</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>5F2D</Tag>
            <Value>656E</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>91</Tag>
            <Value>487711B26810EB1D3030</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F5B</Tag>
            <Value />
          </EmvTagType>
          <EmvTagType>
            <Tag>9F0D</Tag>
            <Value>FC688C9800</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F0E</Tag>
            <Value>0010000000</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F0F</Tag>
            <Value>FC68FC9800</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>9F4C</Tag>
            <Value />
          </EmvTagType>
          <EmvTagType>
            <Tag>9F6B</Tag>
            <Value />
          </EmvTagType>
          <EmvTagType>
            <Tag>9F6C</Tag>
            <Value />
          </EmvTagType>
          <EmvTagType>
            <Tag>9F6E</Tag>
            <Value />
          </EmvTagType>
          <EmvTagType>
            <Tag>9F7C</Tag>
            <Value />
          </EmvTagType>
          <EmvTagType>
            <Tag>DF78</Tag>
            <Value>238420431483</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>DF79</Tag>
            <Value>312E302E3038</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>RCDE</Tag>
            <Value> 00</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>RMSG</Tag>
            <Value> APPROVAL 05157C</Value>
          </EmvTagType>
          <EmvTagType>
            <Tag>8A</Tag>
            <Value>3030</Value>
          </EmvTagType>
        </EmvTags>
      </EmvResponse>
      <EMVReceipt>
        <MerchantName>Sage Payment Solutions - EMV Test</MerchantName>
        <Merchant_Address>1750 Old Meadow Road Suite 300</Merchant_Address>
        <Merchant_City>McLean</Merchant_City>
        <Merchant_State>VA</Merchant_State>
        <Merchant_ZIP>22102</Merchant_ZIP>
        <Merchant_CustServNum>(703) 848-2980</Merchant_CustServNum>
        <TransactionDateTime>2017-08-31T14:45:05</TransactionDateTime>
        <TransactionResult>A</TransactionResult>
        <TransactionID>587243675058086</TransactionID>
        <TransactionType>Purchase</TransactionType>
        <VANReference>C8VHEk4IZ0</VANReference>
        <CardNumber>XXXXXXXXXXXX6824</CardNumber>
        <UTI />
        <EntryModeCode>G</EntryModeCode>
        <EntryModeText>Chip Card</EntryModeText>
        <CVM>SIGN</CVM>
        <AuthMessage>APPROVED 05157C</AuthMessage>
        <AuthCode>05157C</AuthCode>
        <TaxAmount>0</TaxAmount>
        <ShippingAmount>0</ShippingAmount>
        <TotalAmount>0.02</TotalAmount>
        <IsSignatureLineRequired>true</IsSignatureLineRequired>
        <AID>A0000000031010</AID>
        <TVR>0800008000</TVR>
        <IAD>06010A03A02000</IAD>
        <TSI>E800</TSI>
        <ARC>3030</ARC>
        <APN>CHASE VISA</APN>
        <tag9F11>01</tag9F11>
        <tag50>VISA CREDIT</tag50>
        <tag5F2A>0840</tag5F2A>
        <tag5F34>02</tag5F34>
        <tag82>3C00</tag82>
        <tag95>0800008000</tag95>
        <tag9A>170831</tag9A>
        <tag9C>00</tag9C>
        <tag9F02>000000000002</tag9F02>
        <tag9F03>000000000000</tag9F03>
        <tag9F07 />
        <tag9F0D>FC688C9800</tag9F0D>
        <tag9F0E>0010000000</tag9F0E>
        <tag9F0F>FC68FC9800</tag9F0F>
        <tag9F10>06010A03A02000</tag9F10>
        <tag9F12>43484153452056495341</tag9F12>
        <tag9F1A>0840</tag9F1A>
        <tag9F26>FD1646B609FC3A7B</tag9F26>
        <tag9F27>80</tag9F27>
        <tag9F34>5E0000</tag9F34>
        <tag9F36>0063</tag9F36>
        <tag9F37>D008BF7A</tag9F37>
        <IsTransactionReversed>false</IsTransactionReversed>
      </EMVReceipt>
    </PaymentResponseType>
  </PaymentResponses>
</Response_v1>
```
