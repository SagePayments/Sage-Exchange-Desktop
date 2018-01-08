# XML Request and Response Message Formats
The following sections provide examples of the XML request and response messages that you will use to communicate with SED:
1. [Request](#Request)
2. [Response](#Response)

### <a name="Request"></a> Request
Use the following XML message format to send a request to SED:

```xml
<?xml version="1.0" encoding="utf-8"?>
<xs:schema elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="Request_v1" nillable="true" type="Request_v1" />
  <xs:complexType name="Request_v1">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Application" type="ApplicationType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Payments" type="ArrayOfPaymentType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Batch" type="BatchType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionStatusQueries" type="ArrayOfTransactionStatusQueryType" />
      <xs:element minOccurs="0" maxOccurs="1" name="RecurringStatusQueries" type="ArrayOfRecurringStatusQueryType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultStatusQuery" type="VaultStatusQueryType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultOperation" type="VaultOperationType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultAccount" type="VaultAccountType" />
      <xs:element minOccurs="0" maxOccurs="1" name="UI" type="UIType" />
      <xs:element minOccurs="0" maxOccurs="1" name="AccountQuery" type="AccountQueryType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Postback" type="PostbackType" />
      <xs:element minOccurs="1" maxOccurs="1" name="IsSplitPayment" type="xs:boolean" />
      <xs:element minOccurs="0" maxOccurs="1" name="HealthcareSignature" type="HeathcareSignatureType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TerminalItemListDisplay" type="TerminalItemListDisplayType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TerminalDebitConfiguration" type="TerminalDebitConfigurationType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ApplicationType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="ApplicationID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="LanguageID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ClientID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ClientKey" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfPaymentType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="PaymentType" nillable="true" type="PaymentType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="PaymentType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Client" type="ClientType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionBase" type="TransactionBaseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Customer" type="PersonType" />
      <xs:element minOccurs="0" maxOccurs="1" name="ShippingRecipient" type="PersonType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Level2" type="Level2Type" />
      <xs:element minOccurs="0" maxOccurs="1" name="Level3" type="Level3Type" />
      <xs:element minOccurs="0" maxOccurs="1" name="Recurring" type="RecurringType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultStorage" type="VaultStorageType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Postback" type="PostbackType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Healthcare" type="HealthcareType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ClientType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Key" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ID" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="MerchantType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="MerchantID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="MerchantKey" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="TransactionBaseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionType" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Reference1" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Amount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="AuthCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="VANReference" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="CustomerType" type="CustomerType" />
      <xs:element minOccurs="1" maxOccurs="1" name="TenderType" type="TransactionTenderType" />
    </xs:sequence>
  </xs:complexType>
  <xs:simpleType name="CustomerType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="Business" />
      <xs:enumeration value="Consumer" />
      <xs:enumeration value="Government" />
      <xs:enumeration value="Undetermined" />
    </xs:restriction>
  </xs:simpleType>
  <xs:simpleType name="TransactionTenderType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="CREDITCARD" />
      <xs:enumeration value="VIRTUALCHECK" />
      <xs:enumeration value="CASH" />
      <xs:enumeration value="UNDETERMINED" />
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="PersonType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Name" type="NameType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Address" type="AddressType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Company" type="CompanyType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="NameType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="FirstName" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="MI" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="LastName" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="AddressType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="AddressLine1" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="AddressLine2" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="City" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="State" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ZipCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Country" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="EmailAddress" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Telephone" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Fax" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="CompanyType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Name" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Address" type="AddressType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="Level2Type">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="CustomerNumber" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="TaxAmount" type="xs:double" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="Level3Type">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Level2" type="Level2Type" />
      <xs:element minOccurs="1" maxOccurs="1" name="ShippingAmount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="DestinationZipCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="DestinationCountryCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="VATNumber" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="DiscountAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="DutyAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="NationalTaxAmount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="VATInvoiceNumber" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="VATTaxAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="VATTaxRate" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="LineItems" type="ArrayOfLevel3LineItemType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfLevel3LineItemType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="Level3LineItemType" nillable="true" type="Level3LineItemType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="Level3LineItemType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="CommodityCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Description" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ProductCode" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Quantity" type="xs:int" />
      <xs:element minOccurs="0" maxOccurs="1" name="UnitOfMeasure" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="UnitCost" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="TaxAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="TaxRate" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="DiscountAmount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="AlternateTaxIdentifier" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="TaxTypeApplied" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="DiscountIndicator" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="NetGrossIndicator" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="ExtendedItemAmount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="DebitCreditIndicator" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="RecurringType">
    <xs:sequence>
      <xs:element minOccurs="1" maxOccurs="1" name="Schedule" type="ScheduleType" />
      <xs:element minOccurs="1" maxOccurs="1" name="Interval" type="xs:int" />
      <xs:element minOccurs="1" maxOccurs="1" name="DayOfMonth" type="xs:int" />
      <xs:element minOccurs="0" maxOccurs="1" name="StartDate" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Amount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="TimesToProcess" type="xs:int" />
      <xs:element minOccurs="1" maxOccurs="1" name="NonBusinessDay" type="NonBusinessDayType" />
    </xs:sequence>
  </xs:complexType>
  <xs:simpleType name="ScheduleType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="MONTHLY" />
      <xs:enumeration value="DAILY" />
    </xs:restriction>
  </xs:simpleType>
  <xs:simpleType name="NonBusinessDayType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="THATDAY" />
      <xs:enumeration value="BEFORE" />
      <xs:enumeration value="AFTER" />
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="VaultStorageType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="GUID" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Service" type="VaultServiceType" />
    </xs:sequence>
  </xs:complexType>
  <xs:simpleType name="VaultServiceType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="CREATE" />
      <xs:enumeration value="UPDATE" />
      <xs:enumeration value="RETRIEVE" />
      <xs:enumeration value="DELETE" />
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="PostbackType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="HttpsUrl" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="HealthcareType">
    <xs:sequence>
      <xs:element minOccurs="1" maxOccurs="1" name="HealthcareAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="ClinicAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="DentalAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="VisionAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="PerscriptionAmount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="IIASVerification" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="HealthcareItems" type="ArrayOfHealthcareItemType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfHealthcareItemType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="HealthcareItemType" nillable="true" type="HealthcareItemType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="HealthcareItemType">
    <xs:sequence>
      <xs:element minOccurs="1" maxOccurs="1" name="Quantity" type="xs:int" />
      <xs:element minOccurs="0" maxOccurs="1" name="Description" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Price" type="xs:double" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="BatchType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="1" maxOccurs="1" name="Net" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="Count" type="xs:int" />
      <xs:element minOccurs="1" maxOccurs="1" name="BatchPayment" type="BatchPaymentType" />
    </xs:sequence>
  </xs:complexType>
  <xs:simpleType name="BatchPaymentType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="CREDITCARD" />
      <xs:enumeration value="PURCHASECARD" />
      <xs:enumeration value="VIRTUALCHECK" />
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="ArrayOfTransactionStatusQueryType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="TransactionStatusQueryType" nillable="true" type="TransactionStatusQueryType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="TransactionStatusQueryType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionID" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfRecurringStatusQueryType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="RecurringStatusQueryType" nillable="true" type="RecurringStatusQueryType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="RecurringStatusQueryType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="0" maxOccurs="1" name="RecurringID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="StartDate" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="EndDate" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultStatusQueryType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultID" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultOperationType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="VaultID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultStorage" type="VaultStorageType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultAccountType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Company" type="CompanyType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Contact" type="PersonType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="SinglePayment" type="SinglePaymentUIType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultOperation" type="VaultOperationUIType" />
      <xs:element minOccurs="0" maxOccurs="1" name="UIStyle" type="UIStyleType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="SinglePaymentUIType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionBase" type="UITransactionBaseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Customer" type="UIPersonType" />
      <xs:element minOccurs="1" maxOccurs="1" name="PostAuthorizationAnalysis" type="xs:boolean" />
      <xs:element minOccurs="1" maxOccurs="1" name="CVVPrompt" type="xs:boolean" />
      <xs:element minOccurs="1" maxOccurs="1" name="CanEditAccount" type="xs:boolean" />
      <xs:element minOccurs="1" maxOccurs="1" name="CanUseExternalDevice" type="xs:boolean" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UITransactionBaseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Reference1" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Amount" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TaxAmount" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="ShippingAmount" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="AuthCode" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="CustomerNumber" type="UIFieldType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIFieldType">
    <xs:sequence>
      <xs:element minOccurs="1" maxOccurs="1" name="Enabled" type="xs:boolean" />
      <xs:element minOccurs="1" maxOccurs="1" name="Visible" type="xs:boolean" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIPersonType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Name" type="UINameType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Address" type="UIAddressType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UINameType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="FirstName" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="MI" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="LastName" type="UIFieldType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIAddressType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="AddressLine1" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="AddressLine2" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="City" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="State" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="ZipCode" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Country" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="EmailAddress" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Telephone" type="UIFieldType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Fax" type="UIFieldType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultOperationUIType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="AccountNumber" type="UIFieldType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIStyleType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Wizard" type="UIElementStyleType" />
      <xs:element minOccurs="0" maxOccurs="1" name="WizardTitle" type="UIElementStyleType" />
      <xs:element minOccurs="0" maxOccurs="1" name="WizardStepLeft" type="UIElementStyleType" />
      <xs:element minOccurs="0" maxOccurs="1" name="WizardStepRight" type="UIElementStyleType" />
      <xs:element minOccurs="0" maxOccurs="1" name="WizardSupport" type="UIWizardSupportStyle" />
      <xs:element minOccurs="0" maxOccurs="1" name="Buttons" type="UIElementStyleType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIElementStyleType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="BorderStyle" type="UIBorderStyleType" />
      <xs:element minOccurs="0" maxOccurs="1" name="BackgroundColor" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="LabelStyle" type="UIFontStyleType" />
      <xs:element minOccurs="0" maxOccurs="1" name="FieldStyle" type="UIFontStyleType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIBorderStyleType">
    <xs:sequence>
      <xs:element minOccurs="1" maxOccurs="1" name="BorderTop" type="xs:int" />
      <xs:element minOccurs="1" maxOccurs="1" name="BorderBottom" type="xs:int" />
      <xs:element minOccurs="1" maxOccurs="1" name="BorderLeft" type="xs:int" />
      <xs:element minOccurs="1" maxOccurs="1" name="BorderRight" type="xs:int" />
      <xs:element minOccurs="0" maxOccurs="1" name="BorderColor" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIFontStyleType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Family" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Color" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Size" type="xs:int" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="UIWizardSupportStyle">
    <xs:complexContent mixed="false">
      <xs:extension base="UIElementStyleType">
        <xs:sequence>
          <xs:element minOccurs="1" maxOccurs="1" name="Visible" type="xs:boolean" />
        </xs:sequence>
      </xs:extension>
    </xs:complexContent>
  </xs:complexType>
  <xs:complexType name="AccountQueryType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="HeathcareSignatureType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VANReference" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="SignatureAttributes" type="ArrayOfSignatureAttributeType" />
      <xs:element minOccurs="0" maxOccurs="1" name="ItemsDisplayText" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Items" type="ArrayOfString" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfSignatureAttributeType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="SignatureAttributeType" nillable="true" type="SignatureAttributeType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="SignatureAttributeType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Name" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="DisplayText" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfString">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="string" nillable="true" type="xs:string"/>
      </xs:sequence>
    </xs:complexType>
    <xs:complexType name="TerminalItemListDisplayType">
      <xs:sequence>
        <xs:element minOccurs="0" maxOccurs="1" name="TerminalDisplayItems" type="ArrayOfTerminalDisplayItemType" />
      </xs:sequence>
    </xs:complexType>
    <xs:complexType name="ArrayOfTerminalDisplayItemType">
      <xs:sequence>
        <xs:element minOccurs="0" maxOccurs="unbounded" name="TerminalDisplayItemType" nillable="true" type="TerminalDisplayItemType" />
        </xs:sequence>
      </xs:complexType>
      <xs:complexType name="TerminalDisplayItemType">
        <xs:sequence>
          <xs:element minOccurs="1" maxOccurs="1" name="Quantity" type="xs:int" />
          <xs:element minOccurs="0" maxOccurs="1" name="Description" type="xs:string" />
          <xs:element minOccurs="1" maxOccurs="1" name="Price" type="xs:double" />
        </xs:sequence>
      </xs:complexType>
      <xs:complexType name="TerminalDebitConfigurationType">
        <xs:sequence>
          <xs:element minOccurs="1" maxOccurs="1" name="AllowDebit" type="xs:boolean" />
          <xs:element minOccurs="0" maxOccurs="1" name="CashbackAmounts" type="ArrayOfInt" />
        </xs:sequence>
      </xs:complexType>
      <xs:complexType name="ArrayOfInt">
        <xs:sequence>
          <xs:element minOccurs="0" maxOccurs="unbounded" name="int" type="xs:int" />
        </xs:sequence>
      </xs:complexType>
    </xs:schema>
```

### <a name="Response"></a> Response
SED sends response messages using the following XML message format:

```XML
<?xml version="1.0" encoding="utf-8"?>
<xs:schema elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="Response_v1" nillable="true" type="Response_v1" />
  <xs:complexType name="Response_v1">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="PaymentResponses" type="ArrayOfPaymentResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="BatchResponse" type="BatchResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionStatusQueryResponses" type="ArrayOfTransactionStatusQueryResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="RecurringStatusQueryResponses" type="ArrayOfRecurringStatusQueryResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultStatusQueryResponse" type="VaultStatusQueryResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultResponse" type="VaultResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultAccountResponse" type="VaultAccountResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="AccountQueryResponse" type="AccountQueryResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TerminalItemListDisplayResponse" type="TerminalItemListDisplayResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="HealthcareSignatureResponse" type="HealthcareSignatureResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfPaymentResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="PaymentResponseType" nillable="true" type="PaymentResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="PaymentResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultResponse" type="VaultResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="RecurringResponse" type="RecurringResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionResponse" type="TransactionResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Customer" type="PersonType" />
      <xs:element minOccurs="0" maxOccurs="1" name="ShippingRecipient" type="PersonType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Receipt" type="ReceiptType" />
      <xs:element minOccurs="0" maxOccurs="1" name="EmvResponse" type="EmvResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="ResponseIndicator" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ResponseCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ResponseMessage" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="GUID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ExpirationDate" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Last4" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="PaymentDescription" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="PaymentTypeID" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="RecurringResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="RecurringID" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="TransactionResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="AuthCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="AVSResult" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="CVVResult" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="VANReference" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Last4" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="PaymentDescription" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Amount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="PaymentTypeID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Reference1" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionDate" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="AuxiliaryData" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="EntryMode" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="TaxAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="ShippingAmount" type="xs:double" />
      <xs:element minOccurs="0" maxOccurs="1" name="CustomerNumber" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ACHTransactionClass" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="TransactionPaymentType" type="TransactionPaymentType" />
      <xs:element minOccurs="1" maxOccurs="1" name="CashbackAmount" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="FSACard" type="xs:boolean" />
      <xs:element minOccurs="0" maxOccurs="1" name="NetworkID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="CardExpirationDate" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="CurrencyCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="SignatureImageData" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="SignatureFormat" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:simpleType name="TransactionPaymentType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="CREDITCARD" />
      <xs:enumeration value="PURCHASECARD" />
      <xs:enumeration value="VIRTUALCHECK" />
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="PersonType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Name" type="NameType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Address" type="AddressType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Company" type="CompanyType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="NameType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="FirstName" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="MI" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="LastName" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="AddressType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="AddressLine1" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="AddressLine2" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="City" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="State" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ZipCode" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Country" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="EmailAddress" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Telephone" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Fax" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="CompanyType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Name" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Address" type="AddressType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ReceiptType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="ReceiptID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ReceiptTypeID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Language" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="ReceiptData" type="ArrayOfNameValueType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfNameValueType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="NameValueType" nillable="true" type="NameValueType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="NameValueType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Name" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Value" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="EmvResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="EmvTags" type="ArrayOfEmvTagType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfEmvTagType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="EmvTagType" nillable="true" type="EmvTagType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="EmvTagType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Tag" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="Value" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="BatchResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="BatchNumber" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="BatchReference" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="Net" type="xs:double" />
      <xs:element minOccurs="1" maxOccurs="1" name="Count" type="xs:int" />
      <xs:element minOccurs="1" maxOccurs="1" name="BatchPayment" type="BatchPaymentType" />
    </xs:sequence>
  </xs:complexType>
  <xs:simpleType name="BatchPaymentType">
    <xs:restriction base="xs:string">
      <xs:enumeration value="CREDITCARD" />
      <xs:enumeration value="PURCHASECARD" />
      <xs:enumeration value="VIRTUALCHECK" />
    </xs:restriction>
  </xs:simpleType>
  <xs:complexType name="ArrayOfTransactionStatusQueryResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="TransactionStatusQueryResponseType" nillable="true" type="TransactionStatusQueryResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="TransactionStatusQueryResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultResponse" type="VaultResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="RecurringResponse" type="RecurringResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionResponse" type="TransactionResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionSettlementStatus" type="TransactionSettlementStatusType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Customer" type="PersonType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="TransactionSettlementStatusType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionType" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="SettlementType" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="SettlementDate" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="BatchReference" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfRecurringStatusQueryResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="RecurringStatusQueryResponseType" nillable="true" type="RecurringStatusQueryResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="RecurringStatusQueryResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Responses" type="ArrayOfResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionResponses" type="ArrayOfTransactionResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="TransactionSettlementStatuses" type="ArrayOfTransactionSettlementStatusType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Customers" type="ArrayOfPersonType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="ResponseType" nillable="true" type="ResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfTransactionResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="TransactionResponseType" nillable="true" type="TransactionResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfTransactionSettlementStatusType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="TransactionSettlementStatusType" nillable="true" type="TransactionSettlementStatusType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfPersonType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="PersonType" nillable="true" type="PersonType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultStatusQueryResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultResponse" type="VaultResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultAccountResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="VaultAccount" type="VaultAccountType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="VaultAccountType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Company" type="CompanyType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Contact" type="PersonType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="MerchantType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="MerchantID" type="xs:string" />
      <xs:element minOccurs="0" maxOccurs="1" name="MerchantKey" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="AccountQueryResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Merchant" type="MerchantType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Company" type="CompanyType" />
      <xs:element minOccurs="0" maxOccurs="1" name="Services" type="ArrayOfString" />
      <xs:element minOccurs="1" maxOccurs="1" name="Active" type="xs:boolean" />
      <xs:element minOccurs="0" maxOccurs="1" name="CreditCardTerminalConfigurations" type="ArrayOfGatewayTerminalConfiguration" />
      <xs:element minOccurs="0" maxOccurs="1" name="VirtualCheckTerminalConfigurations" type="ArrayOfGatewayTerminalConfiguration" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfString">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="string" nillable="true" type="xs:string"/>
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="ArrayOfGatewayTerminalConfiguration">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="unbounded" name="GatewayTerminalConfiguration" nillable="true" type="GatewayTerminalConfiguration" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="GatewayTerminalConfiguration">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="TerminalNumber" type="xs:string" />
      <xs:element minOccurs="1" maxOccurs="1" name="FrontEndId" type="xs:int" />
      <xs:element minOccurs="0" maxOccurs="1" name="Settings" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="TerminalItemListDisplayResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
    </xs:sequence>
  </xs:complexType>
  <xs:complexType name="HealthcareSignatureResponseType">
    <xs:sequence>
      <xs:element minOccurs="0" maxOccurs="1" name="Response" type="ResponseType" />
      <xs:element minOccurs="0" maxOccurs="1" name="ImageData" type="xs:string" />
    </xs:sequence>
  </xs:complexType>
</xs:schema>

```
