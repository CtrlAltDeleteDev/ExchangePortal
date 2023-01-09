<template>
  <section class="welcome-section">
    <!-- <div class="chat-form">
				<a id="chat-link" class="chat-link focus-in-contract">
					<p class="chat-link-text"> Write to us, we are online! </p>
					<img src="../assets/img/leave.png" class="chat-link-img"
				/></a>

				<div id="chat" class="chat hidden">
					<div class="chat-messages">sdadas</div>
					<div class="chat-textarea-wrapper"><textarea></textarea></div>
				</div>
			</div> -->

    <div class="exchange-container width75persent">
      <h1>Currency exchange</h1>

      <p>Exchange Payeer USD to Bitcoin BTC</p>

      <div class="attention-container">
        <h5>Attention</h5>
        <p>
          The exchange rate is not fixed and is calculated at the time of
          crediting coins to the wallet of the exchange service, the payment is
          usually made within 15 minutes during working hours (from 08-00 to
          24-00 GMT +3).
        </p>
      </div>

      <form class="exchange-form form">
        <h2>Send</h2>

        <div class="exchange-form-row">
          <p class="exchange-rate-text">
            Exchange rate: 100 USD = 0.00562828 BTC
          </p>
          <p class="exchange-min-max-text">
            min.: 1 776.7417 USD, max.: 17 767.4174 USD
          </p>
        </div>

        <div class="exchange-form-row">
          <div class="exchange-currency-container">
            <div class="currency-card-exchange background-white">
              <img src="../assets/img/bitcoin.png" />
              <p>Bitcoin</p>
              <p>BTC</p>
            </div>
          </div>

          <div class="exchange-amount">
            <span>Amount <span class="red">*</span></span>
            <div class="exchange-amount-input-container">
              <input placeholder="" value="100" />
              <div class="exchange-amount-input-container-after">
                min.: 1 776.7417 USD
              </div>
            </div>
          </div>
        </div>

        <div class="white-line"></div>

        <h2>Recieve</h2>

        <div class="exchange-form-row">
          <div class="exchange-currency-container">
            <div class="currency-card-exchange background-white">
              <img src="../assets/img/bitcoin.png" />
              <p>Bitcoin</p>
              <p>BTC</p>
            </div>
          </div>

          <div class="exchange-amount">
            <span>Amount <span class="red">*</span></span>
            <div class="exchange-amount-input-container">
              <input placeholder="" value="100" />
              <div class="exchange-amount-input-container-after">
                min.: 1 776.7417 USD
              </div>
            </div>
          </div>
        </div>

        <label
          >Bitcoin wallet address <b class="red">*</b>:
          <input class="exchange-form-input" />
        </label>

        <div class="white-line"></div>

        <h2>Personal data</h2>

        <label
          >E-mail <b class="red">*</b>:
          <input class="exchange-form-input" />
        </label>

        <button
          @click="$router.push('/preview/create-order')"
          class="exchange-btn background-blue"
          type="button"
        >
          Exchange2
        </button>

        <p class="exchange-form-final-text">Do not remember entered data</p>
      </form>

      <div class="exchange-info-text">
        <h2>Exchange Payeer USD to Bitcoin BTC</h2>
        <p>To exchange you need to follow several steps:</p>
        <ul>
          <li>
            Fill in all the fields of the submitted form. Click the “Exchange”
            button.
          </li>
          <li>
            Read the terms of the contract for the provision of exchange
            services, if you accept them, put a check in the appropriate field
            and click the “Create request”
          </li>
          <li>
            Pay for the application. To do this, transfer the required amount by
            following the instructions on our website.
          </li>
          <li>
            After performing these steps, the systеm will transfer you to the
            “Application Status” page, where the status of your transfer will be
            indicated
          </li>
          <li>To perform this operation, operator participation is required</li>
        </ul>
      </div>
    </div>

    <div class="welcome-bottom-block">
      <div class="welcome-bottom-block-item">
        <img src="../assets/img/bitcoinCursor.png" />

        <h2>Choose currency</h2>
        <p>
          Operations on the service are carried out in automatic and
          semi-automatic modes, guaranteed time for processing applications is
          5-30 minutes
        </p>
      </div>

      <div class="welcome-bottom-block-arrow">
        <img src="../assets/img/arrow.png" />
      </div>

      <div class="welcome-bottom-block-item">
        <img src="../assets/img/phoneCircle.png" />

        <h2>Exchange</h2>
        <p>
          A full range of functions provides a safe conversion of electronic
          currencies, a convenient service that does not cause questions and
          difficulties when performing an exchange
        </p>
      </div>

      <div class="welcome-bottom-block-arrow">
        <img src="../assets/img/arrow.png" />
      </div>

      <div class="welcome-bottom-block-item">
        <img src="../assets/img/bitcoinWithHand.png" />

        <h2>Receive your funds</h2>
        <p>
          A full range of functions provides a safe conversion of electronic
          currencies, a convenient service that does not cause questions and
          difficulties when performing an exchange
        </p>
      </div>
    </div>
  </section>
</template>

<script>
export default {};
</script>

<style scoped></style>
