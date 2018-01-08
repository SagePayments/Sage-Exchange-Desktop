## XML Field Definitions
Sage Exchange uses XML to send request and response messages. This document provides definitions of the XML elements used in both request and response messages:
1. [Request Field Definitions](#Request)
1. [Response Field Definitions](#Response)

### <a name="Request"></a> Request Field Definitions
The sections below define the following elements used in XML request messages:

1. [Request_V1](#Request_V1)
1. [Application](#Application)
1. [Payments](#Payments)
1. [Batch](#Batch)
1. [BatchType](#BatchType)
1. [HealthcareSignature](#HealthcareSignature)
1. [PaymentType](#PaymentType)
1. [Merchant](#Merchant)
1. [TransactionBase](#TransactionBase)
1. [Customer](#Customer)
1. [ShippingRecipient](#ShippingRecipient)
1. [Level2](#Level2)
1. [Level3](#Level3)
1. [Level3LineItemType](#Level3LineItemType)
1. [VaultStorage](#VaultStorage)
1. [Recurring](#Recurring)
1. [Postback](#Postback)

#### <a name="Request_V1"></a> Request_V1 Element - required

```xml
<Request_V1>
  <Application></Application>
  <Payments></Payments>
  <HealthcareSignature></HealthcareSignature>
  <IsSplitPayment></IsSplitPayment>
</Request_V1>
```

The Request_V1 element is the root.  It contains the Application, Payments, HealthcareSignature, and IsSplitPayment elements.

| Element Name | Data Type | Required | Comments |
| ---- | ---- | ---- | ---- |
| Application |	ApplicationType |	Yes |	Identifies the calling application, version, and its certification as a valid integrated solution. |
| Payments |	Array of PaymementType | 	No |	An array of payments to be processed. |
| HealthcareSignature |	HealthcareSignatureType | No |	Used for capturing a signature for healthcare transactions. *Only supported on specific hardware terminals.* |
| IsSplitPayment |	boolean |	No |	Indicates if the array of payments is to be treated as split payments or multi payments. If omitted the default is false. |

#### <a name="Application"></a> Application Element - required

```xml
<Application>
  <ApplicationID></ApplicationID>
  <LanguageID></LanguageID>
  <ClientID></ClientID>
  <ClientKey></ClientKey>
</Application>
```

The Application element contains the ApplicationID, LanguageID, ClientID, and ClientKey elements.

| Element Name | Data Type   | Length   | Required       | Comments |
| ---- | ---- | ---- | ---- | ---- |
| ApplicationID | string           | 50            | Yes                 | Identifies the calling application, version, and its certification as a valid integrated solution. The value is obtained from Sage Payment Solutions through a registration/certification process.                                                               |
| LanguageID | string           | 10            | No                  | Specifies the language to be used when displaying the user interface. The default is “en-US” for English United States. The values are derived from the lower case 2 letter language code from ISO 699-1 and the two letter upper case from ISO 3166. For example,  “fr-CA” = French Canadian. |
| ClientID          | string           | 1-50          | Required for EMV   | A Sage-provided value that identifies the calling application during EMV transactions. |
| ClientKey         | string           | 1-50          | Required for EMV   | A Sage-provided value that identifies the calling application during EMV transactions. |

#### <a name="Payments"></a> Payments Element - optional
```xml
<Payments>
  <PaymentType></PaymentType>
</Payments>
```
The Payments element can contain one or more PaymentType elements. When more than one payment is required to be processed by the calling application multiple PaymentType elements are used. When only a single payment is required, only a single PaymentType element is used. At least one PaymentType element is required in a payment request message.

#### <a name="Batch"></a> Batch Element - optional

```xml
<Batch>
  <BatchType></BatchType>
</Batch>
```

The Batch element can contain one BatchType element.

#### <a name="BatchType"></a> BatchType Element

```xml
<BatchType>
  <Merchant></Merchant>
  <Net></Net>
  <Count></Count>
  <BatchPayment></BatchPayment>
</BatchType>
```
The BatchType element refers to a single payment and must contain a Merchant and BatchPayment element and may contain the optional Net and Count elements.

| Element Name  | Data Type      | Required    | Comments                                                            |
|-------------------|--------------------|-----------------|-------------------------------------------------------------------------|
| Merchant          | MerchantType       | Yes             | Contains the merchant account elements related to processing a batch.   |
| Net               | decimal            | No              | The transaction total net amount of the batch being settled.             |
| Count             | integer            | No              | The transaction count of the batch being settled.                        |
| BatchPayment      | BatchPaymentType   | Yes             | The payment type of batch being settled: </br> <ul><li> CREDITCARD = Standard credit card transactions</li> <li> PURCHASECARD = Level III qualified transactions </li> <li>VIRTUALCHECK = ACH transactions </li></ul> |

#### <a name="HealthcareSignature"></a> HealthcareSignature Element - optional

```xml
<HealthcareSignature>
  <Merchant></Merchant>
  <VANReference> </VANReference>
  <SignatureAttributes></SignatureAttributes>
  <ItemsDisplayText></ItemsDisplayText>
  <Items></Items>
</HealthcareSignature>
```

The HealthcareSignature element contains elements required for a hardware terminal to prompt, capture, and store a healthcare signature with a transaction.

| Element Name  | Data Type    | Length    | Required    | Comments                                                              |
|-------------------|------------------|---------------|-----------------|---------------------------------------------------------------------------|
| Merchant          | MerchantType     |               | Yes             | Contains the merchant account elements related to processing a payment.   |
| VANReference      | string           | 10            | Yes             | The reference of the transaction to store the image to.                                                                                  |
| ItemsDisplayText  | string           |               | No              | The text displayed above the items being signed for on the terminal.                                                                     |
| Items             | complexType      |               | No              | An array of strings, each describing an item being signed for. If this is omitted the terminal will skip displaying the list of items.   |

#### <a name="PaymentType"></a> PaymentType Element

```xml
<PaymentType>
  <Merchant></Merchant>
  <TransactionBase></TransactionBase>
  <Customer></Customer>
  <ShippingRecipient></ShippingRecipient>
  <Level2></Level2>
  <Level3></Level3>
  <VaultStorage></VaultStorage>
  <Recurring></Recurring>
  <Postback></Postback>
  <Healthcare></Healthcare>
</PaymentType>
```

The PaymentType element refers to a single payment and must contain a Merchant and TransactionBase element and may contain the optional Customer, ShippingRecipient, Level2, Level3, VaultStorage, Recurring, and Healthcare elements.

| Element Name  | Data Type         | Required    | Comments                                                                                       |
|-------------------|-----------------------|-----------------|----------------------------------------------------------------------------------------------------|
| Merchant          | MerchantType          | Yes             | Contains the merchant account elements related to processing a payment.                            |
| TransactionBase   | TransactionBaseType   | Yes             | Contains the transaction elements related to processing a payment.                                 |
| Customer          | PersonType            | No\*            | Contains the elements related to the person making the payment.                                    |
| ShippingRecipient | PersonType            | No              | Contains the elements related to the person receiving the goods/services related to the payment.   |
| Level2            | Level2Type            | No              | Contains the additional transaction elements related to qualifying for Level II. The Level2 and Level3 elements are mutually exclusive and should not be used together in a payment. |
| Level3            | Level3Type            | No              | Contains the additional transaction elements related to qualifying for Level III. The Level2 and Level3 elements are mutually exclusive and should not be used together in a payment. |
| VaultStorage      | VaultStorageType      | No\*            | Contains the elements related to Storing/Retrieving/Updating data in the vault service. |
| Recurring         | RecurringType         | No              | Contains the elements needed to schedule a payment in the recurring system. |
| Postback          | PostbackType          | No              | Contains the elements related to POSTing transaction response data to a publically available user defined URL. |
| Healthcare        | HealthcareType        | No              | Contains the additional Healthcare elements related to processing FSA / Healthcare transactions. |

\*Element is required for a Sale, Authorization, or Force without a user interface.

#### <a name="Merchant"></a> Merchant Element
```xml
<Merchant>
  <MerchantID></MerchantID>
  <MerchantKey></MerchantKey>
</Merchant>
```
The Merchant element contains the required MerchantID and MerchantKey elements.

| Element Name  | Data Type    | Length    | Required    | Comments                                 |
|-------------------|------------------|---------------|-----------------|----------------------------------------------|
| MerchantID        | string           | 12            | Yes             | Identifies the merchant account on the VAN.   |
| MerchantKey       | string           | 12            | Yes             | Identifies the merchant.    

#### <a name="TransactionBase"></a> TransactionBase Element
```xml
<TransactionBase>
  <TransactionID></TransactionID>
  <TransactionType></TransactionType>
  <Reference1></Reference1>
  <Amount></Amount>
  <AuthCode></AuthCode>
  <VANReference></VANReference>
</TransactionBase>
```

The TransactionBase element contains the TransactionID, TransactionType, Reference1, Amount, AuthCode, TenderType, CustomerType, and VANReference elements.

| Element Name  | Data Type    | Length    | Required    | Comments |
|-------------------|------------------|---------------|-----------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|
| TransactionID     | string           | 32            | Yes             | Identifies the payment for status inquires. In the event the communication is interrupted, a payment response can be queried later using this element.   |
| TransactionType  | integer          | 2             | Yes             | No user interface: <ol><li> = Sale</li><li>= Authorization</li><li>= Capture</li><li>= Void</li><li>= Force</li><li>= Credit</li><li>= Credit without Reference</li></ol> User interface:<ol><li>= Sale</li><li>= Authorization</li><li>= Capture</li><li>= Force</li><li>= Credit</li><li>Credit without Reference</li></ol> |
| Reference1        | string           | 50            | No              | User defined field, like Invoice Number, Purchase Order Number, etc… |
| Amount            | decimal                 |               | No              | Amount of the payment.                                                               |
| AuthCode          | string                  | 6             | No\*            | Authorization Code for force transactions.                                           |
| VANReference      | string                  | 10            | No\*\*          | VAN unique transaction identifier used with credit, capture, or void transactions.   |
| TenderType        | Transaction TenderType                      |               | No              | Used to control the payment type used for payment processing:<ul><li>CREDITCARD = Debit / credit card payment</li><li>CASH = Cash or a miscellaneous payment</li><li> VIRTUALCHECK = ACH payment</li></ul> |
| CustomerType      | CustomerType           |               | No              | The type of customer processing the payment. The default is Consumer:<ul><li> Business = For business to business payments</li><li>Consumer = For consumer to business payments</li><li>Government = For government to business payments</li></ul> |

\*Element is required for a force transaction.

\*\*Element is required for a credit, capture, or void transaction.

#### <a name="Customer"></a> Customer Element: PersonType Element
```xml
<Customer>
  <Name></Name>
  <Address></Address>
  <Company></Company>
</Customer>
```
The Customer element contains the Name, Address, and Company elements for the person making the payment.

| Element Name  | Data Type    | Required   | Comments                                        |
|-------------------|------------------|-----------------|-----------------------------------------------------|
| Name              | NameType         | No              | Contains elements related to the customer’s name.   |
| Address           | AddressType      | No\*            | Contains elements related to the billing address. Used during address verification service for manually keyed transactions.   |
| Company           | CompanyType      | No              | Contains elements related to the customer’s company. |

\*Elements used for credit card AVS processing when the payment is manually keyed.

#### <a name="ShippingRecipient"></a> ShippingRecipient Element: PersonType Element
```xml
<ShippingRecipient> <Name></Name>
  <Address></Address>
  <Company></Company>
</ShippingRecipient>
```
The ShippingRecipient element contains the Name, Address, and Company elements for the person receiving the goods/service.

| Element Name  | Data Type    | Required    | Comments                                            |
|-------------------|------------------|-----------------|---------------------------------------------------------|
| Name              | NameType         | No              | Contains elements related to the recipient’s name.      |
| Address           | AddressType      | No\*            | Contains elements related to the shipping address.      |
| Company           | CompanyType      | No              | Contains elements related to the recipient’s company.   |

\*Elements used for credit card AVS processing when the payment is manually keyed.

#### <a name="Level2"></a> Level2 Element
```xml
<Level2>
  <CustomerNumber></CustomerNumber>
  <TaxAmount></TaxAmount>
</Level2>
```
The Level2 element contains the CustomerNumber and TaxAmount elements used to qualify a purchase card payment for Level II.

| Element Name  | Data Type    | Length    | Required    | Comments                   |
|-------------------|------------------|---------------|-----------------|--------------------------------|
| CustomerNumber    | string           | 17            | Yes             | User defined customer number.  |
| TaxAmount         | decimal          |               | Yes             | The tax amount being charged.  |

#### <a name="Level3"></a> Level3 Element
```xml
<Level3>
  <Level2></Level2>
  <ShippingAmount></ShippingAmount>
  <DestinationZipCode></DestinationZipCode>
  <DestinationCountryCode></DestinationCountryCode>
  <VATNumber></VATNumber>
  <DiscountAmount></DiscountAmount>
  <DutyAmount></DutyAmount>
  <NationalTaxAmount></NationalTaxAmount>
  <VATInvoiceNumber></VATInvoiceNumber>
  <VATTaxAmount></VATTaxAmount>
  <VATTaxRate></VATTaxRate>
  <LineItems></LineItems>
</Level3>
```
The Level3 element contains the optional CustomerNumber, TaxAmount, DestinationZipCode, NationalTax, VATNumber, DiscountAmount, DutyAmount, VATInvoiceNumber, VATTaxAmount, VATTaxRate, DestinationCountryCode, and LineItems elements used to qualify a purchase card payment for Level III.

| Element Name   | Data Type    | Length    | Required    | Comments                                                |
|--------------------|------------------|---------------|-----------------|-------------------------------------------------------------|
| Level2             | string           | 17            | Yes             | Contains the Level2 elements CustomerNumber and TaxAmount.   |
| ShippingAmount     | decimal          |               | Yes             | Shipping amount charged to the transaction. |
| DestinationZipCode | string           | 9             | Yes             | Postal zip code where the goods/services are shipped.        |
| VATNumber          | string           | 13            | Yes             | Customer’s value added tax number.                           |
| DiscountAmount     | decimal          |               | Yes             | Discount amount applied to the transaction.                  |
| DutyAmount         | decimal          |               | Yes             | Duty amount. |
| VATInvoiceNumber   | string           | 15            | Yes             | Value added tax invoice number. |
| VATTaxAmount       | decimal          |               | Yes             | Value added tax amount. |
| VATTaxRate         | decimal          |               | Yes             | Value added tax rate. |
| DestinationCountryCode | integer      |               | Yes             | 3 digit ISO country code where the goods/services are shipped. |
| LineItems          | Array ofLevel3LineItemType  |    | No              | Contains zero or more Level3LineItemType elements. |
| NationalTaxAmount  | decimal           |              | Yes             | National tax amount applied to the transaction. |

#### <a name="Level3LineItemType"></a> Level3LineItemType Element
```xml
<Level3LineItemType>
  <CommodityCode></CommodityCode>
  <Description></Description>
  <ProductCode></ProductCode>
  <Quantity></Quantity>
  <UnitOfMeasure></UnitOfMeasure>
  <UnitCost></UnitCost>
  <TaxAmount></TaxAmount>
  <TaxRate></TaxRate>
  <DiscountAmount></DiscountAmount>
  <AlternateTaxIdentifier></AlternateTaxIdentifier>
  <TaxTypeApplied></TaxTypeApplied>
  <DiscountIndicator></DiscountIndicator>
  <NetGrossIndicator></NetGrossIndicator>
  <ExtendedItemAmount></ExtendedItemAmount>
  <DebitCreditIndicator></DebitCreditIndicator>
</Level3LineItemType>
```
The Level3LineItemType element contains the CommodityCode, Description, ProductCode, Quantity, UnitOfMeasure, UnitCost, TaxAmount, TaxRate, DiscountAmount, AlternateTaxIndentifier, TaxTypeApplied, DiscountIndicator, NetGrossIndicator, ExtendedItemAmount, DebitCreditIndicator, and TotalAmount elements.

| Element Name  | Data Type    | Length    | Required    | Comments                      |
|-------------------|------------------|---------------|-----------------|-----------------------------------|
| CommodityCode     | string           | 12            | Yes             | Commodity code that applies to the item.         |
| Description       | string           | 35            | Yes             | A brief description of the item.   |
| ProductCode       | string           | 12            | Yes             | Product code that applies to the item. |
| Quantity                 | integer          |               | Yes             | Quantity of item(s) purchased. |
| UnitOfMeasure            | string           | 12            | Yes             | Units of measure of the item(s) purchased. |
| UnitCost                 | decimal          |               | Yes             | Cost of the item purchased.                 |
| TaxAmount                | decimal          |               | Yes             | The tax amount for the item.                |
| TaxRate                  | decimal          | 15            | Yes             | The tax rate for the item.                  |
| DiscountAmount           | decimal          |               | Yes             | Discount amount applied to item.            |
| AlerternateTaxIdentifier | string           |               | Yes             | Value added tax rate.                       |
| TaxTypeApplied           | string           | 4             | Yes             |                                            |
| DiscountIndicator       | string           | 1             | Yes             |                                            |
| ExtendedItemAmount       | decimal          |               | Yes             | The total amount of the individual item = ( ItemCost X Quantity ) – ( DiscountAmount x Quantity )   |
| NetGrossIndicator       | string           | 1             | | ||

#### <a name="VaultStorage"></a> VaultStorage Element

```xml
<VaultStorage>
  <Service></Service>
  <GUID></GUID>
</VaultStorage>
```
The VaultStorage element contains the Service, and GUID elements.

| Element Name  | Data Type      | Length    | Required    | Comments  |
|-------------------|--------------------|---------------|-----------------|---------------|
| Service           | VaultServiceType   |               | Yes             | Used to indicate the vault operation:<ul><li>RETRIEVE = Pull data from the vault for processing</li><li> UPDATE = Update data in the vault with new data captured</li><li>CREATE = Insert data in the vault with data captured</li></ul>|
| GUID              | string           | 36            | No\*            | The vault GUID referencing a previous payment account captured in the vault. Payment information will not need to be captured and instead it is retrieved from the vault.   |

\*Required for retrieval and update operations.

#### <a name="Recurring"></a> Recurring Element
```xml
<Recurring>
  <Schedule></Schedule>
  <Interval></Interval>
  <DayOfMonth></DayOfMonth>
  <StartDate></StartDate>
  <Amount></Amount>
  <TimesToProcess></TimesToProcess>
  <NonBusinessDay></NonBusinessDay>
</Recurring>
```
The Recurring element contains the ScheduleType, ScheduleInterval, DayOfMonth, StartDate, Amount, TimesToProcess, and NonBusinessDay elements.

| Element Name  | Data Type    | Length    | Required    | Comments                                  |
|-------------------|------------------|---------------|-----------------|-----------------------------------------------|
| Schedule          | ScheduleType     |               | Yes             | The schedule type for the recurring payment:<ul><li>DAILY = Schedule will be based on day</li><li>MONTHLY = Schedule will be based on month</li></ul>              |
| Interval          | integer          |               | No              | The interval between processing. For example, every other month:<ul><li> Schedule = MONTHLY </li><li> Interval = 2</li></ul> |
| DayOfMonth        | integer              |               | No              | For monthly based schedule this is the day of the month to process the payment.   |
| StartDate         | string               | 10            | No              | The date the payment will start processing in MM/DD/YYYY format.                                                               |
| Amount            | decimal              |               | No              | The amount of the payment, if different than the original payment amount.         |
| TimesToProcess    | integer              |               | No              | The number of payments to process (-1 = Indefinite).                                           |
| NonBusinessDays   | NonBusinessDayType   |               | No              | <ul><li>THATDAY = Payment is processed on the non business day</li><li> BEFORE = Payment is processed before the non business day</li><li> AFTER = payment is processed after the non business day</li></ul>

#### <a name="Postback"></a> Postback Element
```xml
<Postback>
  <HttpsUrl></HttpsUrl>
</Postback>
```
The Postback element refers to a URL in which transaction response data should be sent after processing and must contain an HttpsUrl element.

| Element Name  | Data Type    | Length    | Required    | Comments                                                                                                                                                                            |
|-------------------|------------------|---------------|-----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| HttpsUrl          | string           | 1024          | Yes             | The absolute URL to POST transaction response data back to. The communication requires SSL. This is to be used in integrations in which response handling is disconnected or in which response data should be delivered to a remote server.   |


### <a name="Response"></a> Response field definitions
The following sections define the XML fields used by SED in the response messages it sends:

1. [PaymentResponseType](#PaymentResponseType)
1. [ResponseType](#ResponseType)
1. [VaultResponseType](#VaultResponseType)
1. [RecurringResponseType](#RecurringResponseType)
1. [TransactionResponseType](#TransactionResponseType)
1. [EmvResponseType](#EmvResponseType)
1. [BatchResponseType](#BatchResponseType)
1. [TransactionSettlementStatusType](#TransactionSettlementStatusType)
1. [TransactionStatusQueryResponseType](#TransactionStatusQueryResponseType)
1. [RecurringStatusQueryResponseType](#RecurringStatusQueryResponseType)
1. [VaultAccountResponseType](#VaultAccountResponseType)
1. [AccountQueryResponseType](#AccountQueryResponseType)


#### <a name="PaymentResponseType"></a> PaymentResponseType Element - required
```xml
<PaymentResponseType>
  <Response></Respopnse>
  <VaultResponse></VaultResponse>
  <RecurringResponse></RecurringResponse>
  <Customer></Customer>
  <TransactionResponse></TransactionResponse>
  <EmvResponse></EmvResponse>
</PaymentResponseType>
```
The PaymentResponseType element contains the response elements related to a payment.

| Element Name    | Data Type             | Required    | Comments                                                           |
|---------------------|---------------------------|-----------------|------------------------------------------------------------------------|
| Response            | ResponseType              | Yes             | Contains the response elements related to the gateway request.          |
| VaultResponse       | VaultResponseType         | No              | Contains the response elements related to the vault operation.          |
| RecurringResponse   | RecurringResponseType     | No              | Contains the response elements related to the transaction enlistment into the recurring system.                                             |
| TransactionResponse | TransactionResponseType   | Yes             | Contains the response elements related to processing a transaction on the gateway.                                                               |
| Customer            | PersonType                | Yes             | Contains the billing/customer elements used for the payments.           |
| EmvResponse         | EmvResponseType           | No              | Contains response data specific to EMV transactions.                   |

#### <a name="ResponseType"></a> ResponseType Element - required
```xml
<ResponseType>
  <ResponseIndicator></RespopnseIndicator>
  <ResponseCode></ResponseCode>
  <ResponseMessage></ResponseMessage>
</ResponseType>
```

The ResponseType element contains the gateway response elements related to a gateway request.

| Element Name  | Data Type    | Length    | Required    | Comments                                                                                                   |
|-------------------|------------------|---------------|-----------------|----------------------------------------------------------------------------------------------------------------|
| ResponseIndicator                | string           | 1             | Yes             | The gateway/vault response indicator:<ul><li> A=Approved</li><li> E=Declined</li><li> X=Error</li><li>I = Batch Inquiry (only returned on batch inquiry requests)</li></ul></br>When returned during a Payment request, this field is used to determine the status of a Payment (Approved/Declined/Error). |
| ResponseCode      | string           | 6             | Yes             | The gateway/vault response code. When the indicator is a E or X, this code and be used to determine the cause.   |
| ResponseMessage  | string           | 32            | Yes             | The gateway/vault response text.

#### <a name="VaultResponseType"></a> VaultResponseType Element
```xml
<VaultResponseType>
  <Response></Response>
  <GUID></GUID>
  <ExpirationDate></ExpirationDate>
  <Last4></Last4>
  <PaymentDescription></PaymentDescription>
  <PaymentTypeID></PaymentTypeID>
</VaultResponseType>
```
The VaultResponseType element contains the the response elements related to a vault operation.

| Element Name  | Data Type    | Length    | Required    | Comments  |
|-------------------|------------------|---------------|-----------------|---------------------------------------------------------------|
| Response          | ResponseType    |               | Yes             | Contains the response elements related to the vault request.   |
| GUID              | string           | 36            | Yes             | The vault GUID used to reference the card data captured. |
| ExpirationDate    | string           | 4             | Yes             | The date the account data will expire in  MMYY format. <br/> *This field is only applicable to responses with PaymentTypeID (3,4,5,6,7,D,O).* |
| Last4             | string           |               | Yes             | A masked representation of the account data showing only the last four digits. |
| PaymentDescription              | string           |               | Yes             | The description of the account data. Credit card payments will be the first 6 digits + masked digits + last 4 digits. ACH payments will be the routing number + space + masked account digits + last 4 account digits.  |
| PaymentTypeID     | string           | 1             | Yes             | The payment type identifier: <ul><li>3=American Express</li><li> 4=Visa</li><li>5=MasterCard</li><li>6=Discover</li><li>7=JCB</li><li>D=Debit Card</li><li>O=Other</li><li>C=ACH</li></ul> |

#### <a name="RecurringResponseType"></a> RecurringResponseType Element
```xml
<RecurringResponseType>
  <RecurringID></RecurringID>
</RecurringResponseType>
```
The RecurringResponseType element contains the recurring identifier element to reference the recurring transaction.

| Element Name  | Data Type    | Required    | Comments                              |
|-------------------|------------------|-----------------|-------------------------------------------|
| RecurringID       | string           | Yes             | The recurring record identifier for the recurring transaction listed in the recurring system.   |

#### <a name="TransactionResponseType"></a> TransactionResponseType Element
```xml
<TransactionResponseType>
  <AuthCode></AuthCode>
  <AVSResult></AVSResult>
  <CVVResult></CVVResult>
  <VANReferene></VANReference>
  <TransactionID></TransactionID>
  <Last4></Last4>
  <PaymentDescription></PaymentDescription>
  <Amount></Amount>
  <PaymentTypeID></PaymentTypeID>
  <EntryMode></EntryMode>
  <NetworkID></NetworkID>
  <CardExpirationDate></CardExpirationDate> <CurrencyCode></CurrencyCode>
  <SignatureImageData></SignatureImageData>
  <SignatureFormat></SignatureFormat>
</TransactionResponseType>
```
The TransactionResponseType element contains the response elements related to processing a transaction.

| Element Name  | Data Type    | Required    | Comments                                                                                         |
|-------------------|------------------|-----------------|------------------------------------------------------------------------------------------------------|
| AuthCode          | string           | 6               | The authorization code for the approved payment. |
| AVSResult         | string           | 1               | The AVS result for manually keyed transactions. This field is for information purposes and is not to be used to determine the status of a payment.</br> *This field is only applicable to responses with PaymentTypeID (3,4,5,6,7,D,O).* |
| CVVResult         | string           | 1               | The CVV result for matching the verification value. This field is for information purposes and is not to be used to determine the status of a payment.</br> *This field is only applicable to responses with PaymentTypeID (3,4,5,6,7,D,O).* |
| VANReference        | string           | 10              | The gateway transaction reference to be used later in captures, voids, and credits. |
| TransactionID       | string           | |The unique transaction identifier. |
| Last4               | string           |                 | The last four digits of the card number. |
| PaymentDescription                | string           |                 | The description of the account data.</br> Credit Card payments will be the first 6 digits + masked digits + last 4 digits.</br> ACH payments will be the routing number + space + masked account digits + last 4 account digits. |
| Amount              | decimal          |                 | Amount of the payment. |
| PaymentTypeID       | string           | 1               | The payment type identifier: <ul><li>3=American Express</li><li> 4=Visa</li><li>5=MasterCard</li><li>6=Discover</li><li>7=JCB</li><li>D=Debit Card</li><li>O=Other</li><li>C=ACH</li></ul></br> *Additional values could be added when future payment types become available to the system.*  |
| EntryMode           | String           | 1               | The method of entry:<ul><li>K = Manually Keyed</li><li>H = Track 1</li><li>D = Track 2</li><li>M = MICR Read</li><li>S = Swipe</li><li>I = Insert</li><li>C = Contactless</li><li>F = Fallback swipe</li></ul> |
| NetworkID           | String           | 1               | The ID of the network:<ul><li>P = Paymentech</li><li>T = TSYS</li><li>1.  = Elavon</li><li>2.  = First Data</li><li>S = SPS ACH</li></ul></br> *Additional values could be added when future networks become available to the system.*      |
| CardExpirationDate | string           | 4               | The date the account data will expire in MMYY format. |
| CurrencyCode        | string           | 3               | Represents the transaction currency. USD = 840.                                               |
| SignatureImageData | string           |                 | Base-64 encoded image data representing the transaction signature.                           |
| SignatureFormat                    | string           |                 | The signature image format; eg, “PNG”. |  

#### <a name="EmvResponseType"></a> EmvResponseType Element
```xml
<EmvResponseType>
  <EmvTags></EmvTags>
</EmvResponseType>
```
The EmvResponseType element contains the response elements related to an EMV transaction.

| Element Name  | Data Type    | Length    | Required    | Comments                                                                                                          |
|-------------------|------------------|---------------|-----------------|-----------------------------------------------------------------------------------------------------------------------|
| EmvTags           | EmvTagType              | Array         | Yes             | Contains an array of key/value EmvTagType elements. Please see the EMV-specific documentation for more information.   |

#### <a name="BatchResponseType"></a> BatchResponseType Element
```xml
<BatchResponseType>
  <Response></Response>
  <BatchNumber></BatchNumber>
  <BatchReference></BatchReference>
  <Net></Net>
  <Count></Count>
  <BatchPayment></BatchPayment>
</BatchResponseType>
```
The BatchResponseType element contains the response elements related to processing a batch.

| Element Name  | Data Type       | Length    | Required    | Comments                                                                                             |
|-------------------|---------------------|---------------|-----------------|----------------------------------------------------------------------------------------------------------|
| Response          | Response Type       |               | Yes             | Contains the response elements related to the gateway request. |
| BatchNumber       | string              | 6             | Yes             | The batch sequence number. |
| BatchReference   | string              | 10            | Yes             | The unique gateway batch identifier. |
| Net               | decimal             |               | Yes             | The net transaction amount of the batch. A negative amount is possible when processing credits/refunds.   |
| Count             | integer             |               | Yes             | The total transaction count of the batch. |
| BatchPayment      | BatchPaymentType   |               | Yes             | CREDITCARD <br/> PURCHASECARD |

#### <a name="TransactionSettlementStatusType"></a> TransactionSettlementStatusType Element
```xml
<TransactionSettlementStatusType>
  <TransactionType></TransactionType>
  <SettlementType></SettlementType>
  <SettlementDate></SettlementDate>
  <BatchReference></BatchReference>
</TransactionSettlementStatusType>
```
The TransactionSettlementStatusType element contains the settlement status elements related to a transaction.

| Element Name  | Data Type    | Length    | Required    | Comments                                                      |
|-------------------|------------------|---------------|-----------------|-------------------------------------------------------------------|
| TransactionType  | integer          |               | Yes             | The transaction type of the transaction processed. |
| SettlementType   | integer          |               | Yes             | The settlement type of the transaction processed:<ul><li>0 = Error / Declined</li><li>1= Batch</li><li>2  = Settled</li><li>3  = Expired</li></ul> |
| SettlementDate   | string           |               | No              | The settlement date, if settled, for the transaction processed, in MM/DD/YYYY HH:MM:SS format. |
| BatchReference   | string           | 10            | No              | The batch reference, if settled, for the transaction processed.    |

#### <a name="TransactionStatusQueryResponseType"></a> TransactionStatusQueryResponseType Element
```xml
<TransactionStatusQueryResponseType>
  <Response></Response>
  <VaultResponse></VaultResponse>
  <RecurringResponse></RecurringResponse>
  <TransactionResponse></TransactionResponse>
  <TransactionSettlementStatus></TransactionSettlementStatus>
  <Customer></Customer>
</TransactionStatusQueryResponseType>
```
The TransactionStatusQueryResponseType element contains the response elements related to processing a transaction.

| Element Name             | Data Type              | Required    | Comments                                                                                     |
|------------------------------|----------------------------|-----------------|--------------------------------------------------------------------------------------------------|
| Response                     | ResponseType              | Yes             | Contains the response elements related to the gateway request. |
| VaultResponse                | VaultResponseType        | No              | Contains the response elements related to the vault operation. |
| RecurringResponse           | RecurringResponseType    | No              | Contains the response elements related to the transaction enlistment into the recurring system.   |
| TransactionResponse         | TransactionResponseType                         | Yes             | Contains the response elements related to processing a transaction on the gateway.                |
| TransactionSettlementStatus | TransactionSettlementType                         | Yes             | Contains the transaction settlement status.                                                       |
| Customer                     | PersonType                | Yes             | Contains the billing/customer elements used for the payments. |


#### <a name="RecurringStatusQueryResponseType"></a> RecurringStatusQueryResponseType Element
```xml
<RecurringStatusQueryResponseType>
  <Responses></Responses>
  <TransactionResponses></TransactionResponses>
  <TransactionSettlementStatuses></TransactionSettlementStatuses>
  <Customers></Customers>
</RecurringStatusQueryResponseType>
```
The RecurringStatusQueryResponseType element contains the response elements related to a recurring transaction processed by the Sage system.

| Element Name                | Data Type                 | Length    | Required    | Comments                                                                                     |
|---------------------------------|-------------------------------|---------------|-----------------|--------------------------------------------------------------------------------------------------|
| Responses                       | Response Type                 | Array         | Yes             | Contains the response elements related to the gateway request. |
| TransactonResponses            | TransactionResponseType     | Array         | Yes             | Contains the response elements related to the vault operation.                                    |
| TransactionSettlementStatuses | TransactionSettlementType   | Array         | Yes             | Contains the response elements related to the transaction enlistment into the recurring system.   |
| Customers                       | PersonType                            | Array         | Yes             | Contains the response elements related to processing a transaction on the gateway.                |

#### <a name="VaultAccountResponseType"></a> VaultAccountResponseType Element
```xml
<VaultAccountResponseType>
  <Response></Response>
  <VaultAccount></VaultAccount>
  <Merchant></Merchant>
</VaultAccountResponseType>
```
The RecurringStatusQueryResponseType element contains the response elements related to a recurring transaction processed by the Sage system.

| Element Name  | Data Type                 | Length    | Required    | Comments                                                             |
|-------------------|-------------------------------|---------------|-----------------|--------------------------------------------------------------------------|
| Response          | Response Type                 |               | Yes             | Contains the response elements related to the account creation request.   |
| VaultAccount      | VaultAccountType             |               | Yes             | Contains the account elements provided in the request.                    |
| Merchant          | TransactionSettlementType   | Array         | Yes             | Contains the newly created MerchantID. |

#### <a name="AccountQueryResponseType"></a> AccountQueryResponseType Element
```xml
<AccountQueryResponseType>
  <Response></Response>
  <Merchant></Merchant>
  <Services></Services>
  <Active></Active>
</AccountQueryResponseType>
```
The AccountQueryResponseType element contains the response elements related to a merchant account status and service list inquiry.

| Element Name  | Data Type    | Length    | Required    | Comments                                                                     |
|-------------------|------------------|---------------|-----------------|----------------------------------------------------------------------------------|
| Response          | Response Type    |               | Yes             | Contains the response elements related to the account inquiry request.                                                                         |
| Merchant          | MerchantType    |               | Yes             | Contains the account elements provided in the request.                            |
| Services          | string           | Array         | Yes             | Contains an array of service descriptions available to a merchant account:<ul><li>VAULT = “Vault storage service”</li><li>CREDITCARD = “Credit Card payment service”</li></ul> |
| Active            | boolean          |               | Yes             | The status of the merchant account.                                               |

### Field validation
The following sections define the accepted values for alphanumeric and numeric fields.

XML reserved characters must be observed and encoded appropriately.

#### Alphanumeric fields\*
The following is a list of accepted characters for alphanumeric fields:
* White Space
* a-z
* A-Z
* 0-9
* -.,\#&()/!'éèêëòóôõöàáâãäåìíîïùúûüýÿ

\*The use of two or more dashes -- back to back is not permitted in any of the fields.

#### Numeric fields\*
The following is a list of accepted characters for numeric fields:
* 0-9
* ,.-

\*The use of two or more dashes -- back to back is not permitted in any of the fields.

## Gateway error codes
The following table defines the error codes that may be returned by SED:

| Code  | Message                  | Description                                             |
|-----------|------------------------------|-------------------------------------------------------------|
| 000000    | INTERNAL SERVER ERROR        | Server Error.                                                |
| 900000    | INVALID T\_ORDERNUM          | Order number value is in an invalid format.                  |
| 900001    | INVALID C\_NAME              | Name value is in an invalid format or was left blank.        |
| 900002    | INVALID C\_ADDRESS           | Address value is in an invalid format or was left blank.     |
| 900003    | INVALID C\_CITY              | City value is in an invalid format or was left blank.        |
| 900004    | INVALID C\_STATE             | State value is in an invalid format or was left blank.       |
| 900005    | INVALID C\_ZIP               | Zip code value is in an invalid format or was left blank.    |
| 900006    | INVALID C\_COUNTRY                  | Country value is in an invalid format or was left blank.     |
| 900007    | INVALID C\_TELEPHONE                | Telephone value is in an invalid format or was left blank.   |
| 900008    | INVALID C\_FAX               | Fax value is in an invalid format or was left blank.         |
| 900009    | INVALID C\_EMAIL             | Email value is in an invalid format or was left blank.       |
| 900010    | INVALID C\_SHIP\_NAME               | Shipping address name value is in an invalid format.         |
| 900011    | INVALID\_C\_SHIP\_A DDRESS   | Shipping address value is in an invalid format.              |
| 900012    | INVALID\_C\_SHIP\_CI TY      | Shipping city value is in an invalid format.                 |
| 900013    | INVALID\_C\_SHIP\_S TATE     | Shipping state value is in an invalid format.                |
| 900014    | INVALID\_C\_SHIP\_ZIP        | Shipping zip code value is in an invalid format.             |
| 900015    | INVALID\_C\_SHIP\_C OUNTRY   | Shipping country value is in an invalid format.              |
| 900016    | INVALID\_C\_CARDNUMBER       | Credit card number value is in an invalid format.            |
| 900017    | INVALID\_C\_EXP              | Expiration date value is in an invalid format. |
| 900018    | INVALID\_C\_CVV                       | CVV (card verification value) value is in an invalid format or was left blank (if set to required).    |
| 900019    | INVALID\_T\_AMT                       | Grand total must greater than $0.00. Please check subtotal, shipping, and tax values.                    |
| 900020    | INVALID\_T\_CODE                      | Transaction code value is in an invalid format or was left blank. |
| 900021    | INVALID\_T\_AUTH                      | Authorization code is in an invalid format or was left blank (required for force transactions).        |
| 900022    | INVALID\_T\_REFERE NCE                | Reference value is in an invalid format or was left blank (Required for force or void by reference transactions).   |
| 900023    | INVALID\_T\_TRACKD ATA                | Track data value is in an invalid format or was left blank (required for debit and retail transactions).                                              |
| 900024    | INVALID\_T\_TRACKI NG\_NUMBER         | Tracking number value is in an invalid format. |
| 900025    | INVALID\_T\_CUSTO MER\_NUMBER         | Customer number value is in an invalid format (used only for PCLIII transactions). |
| 900026    | INVALID\_T\_SHIPPIN G\_COMPANY        | Shipping company value is in an invalid format. |
| 900027    | INVALID\_T\_RECUR RING                | Recurring value is in an invalid format (must be = 0 or 1). |
| 900028    | INVALID\_T\_RECUR RING\_TYPE          | Recurring value is in an invalid format. |
| 900029    | INVALID\_T\_RECUR RING\_INTERVAL      | Recurring interval value is in an invalid format (must be numeric). |
| 900030    | INVALID\_T\_RECUR RING\_INDEFINITE    | Recurring indefinite value is in an invalid format or was left blank. |
| 900031    | INVALID\_T\_RECURRING\_TIMES\_TO\_PROCESS                               | Recurring times to process value is in an invalid format (must be numeric).                            |
| 900032    | INVALID\_T\_RECURRING\_NON\_BUSINESS\_DAYS                             | Recurring non business days value is in an invalid format.                                             |
| 900033    | INVALID\_T\_RECUR RING\_GROUP         | Recurring group was left blank or group not found. |
| 900034    | INVALID\_T\_RECUR RING\_START\_DATE   | Recurring start date value is in an invalid format or was left blank. |
| 900035    | INVALID\_T\_PIN          | PIN number entered is incorrect (required for PIN-debit transactions).           |
| 901000    |                          | General data validation error. The message will contain additional information.   |
| 910000    | SERVICE NOT ALLOWED      | The transaction you are trying to submit is not allowed.                         |
| 910001    | VISA NOT ALLOWED         | Visa card type transactions are not allowed.                                     |
| 910002    | MASTERCARD NOT ALLOWED   | Master Card card type transactions are not allowed.                               |
| 910003    | AMEX NOT ALLOWED         | American Express card type transactions are not allowed.                         |
| 910004    | DISCOVER NOT ALLOWED     | Discover card type transactions are not allowed.                                 |
| 910005    | CARD TYPE NOT ALLOWED    | Card type transactions are not allowed.                                          |
| 911911    | SECURITY VIOLATION       | M\_id or M\_key is incorrect. |
| 920000    | ITEM NOT FOUND           | Item not found. |
| 920001    | CERDIT VOL EXCEEDED      | No corresponding sale found within last 6 months. Credit couldn’t be issued.     |
