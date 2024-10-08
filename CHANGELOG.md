# Changelog
All notable changes to this project will be documented in this file.

## [1.2.4]

- Add new response property `AppUrl` for `createPaymentRequest`.
- Add new method `CreditCardWalletInitiateAppPayment` to initiate credit card wallet payment in APP for MobilePay & Vipps.

## [1.2.3]

- Add support for endpoint `calculateSurcharge`.

## [1.2.2]

- Add option to set new callback parameter `callback_mobile_app_redirect` for `createPaymentRequest`.

## [1.2.1]

- Add support for endpoint `getTerminals`.

## [1.2.0]

- Add `digital`, `discount`, `gift_card`, `physical` and `sales_tax` as new values for goodsType.

## [1.1.9]

- Replace the deprecated 'transactions' endpoint with 'payments', for 'GetPayment' & 'GetPayments' methods

## [1.1.8]

- Supports API changes from 20230412
- Enforce the right HTTP methods on all API endpoints.

## [1.1.7]

- Add support for redirect response in reservation, chargeSubscription & reserveSubscriptionCharge

## [1.1.6]

- Add support for subscription via MobilePay

## [1.1.5]

- Fix 'Agreements Engine' parameter 'unscheduled_type'
- Add support for Apple Pay 

## [1.1.4]

- Add support for new 'Agreements Engine' parameters

## [1.1.3]

- Update format of the User-Agent header

## [1.1.2]

- Add docker image to build package
- Supports API changes from 20210324

## [1.1.1]

- Support Product URL for Klarna Payments
