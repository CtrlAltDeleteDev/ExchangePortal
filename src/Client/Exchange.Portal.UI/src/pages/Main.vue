<template>
  <div class="">
    <section class="welcome-section" id="main">
      <div class="welcome-top-block">
        <div class="welcome-section-title">
          <h1>Micro-exchange service for exchanging electronic currencies</h1>
          <h2>Fast and reliable execution of exchanges</h2>
        </div>

        <div class="welcome-section-exchange">
          <div class="welcome-section-exchange-row desctop-element">
            <span class="welcome-section-exchange-title card-width"
              >You send</span
            >
            <span class="welcome-section-exchange-arrows"></span>
            <span class="welcome-section-exchange-title card-width"
              >You receive</span
            >
          </div>

          <div class="welcome-section-exchange-row">
            <span class="welcome-section-exchange-title mobile-element"
              >You send</span
            >
            <div>
              <div
                id="currency_send"
                class="welcome-section-exchange-items card-width"
              >
                <div id="selected_send_curreny">
                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="send"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/BitcoinBTC.png" />
                    <p>Bitcoin</p>
                    <p>BTC</p>
                  </button>
                </div>

                <div id="currency_send_list" class="open-select hidden">
                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="send"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/BitcoinBTC.png" />
                    <p>Bitcoin</p>
                    <p>BTC</p>
                  </button>

                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="send"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/DaiDai.png" />
                    <p>Dai Dai</p>
                    <p></p>
                  </button>

                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="send"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/TronTRX.png" />
                    <p>Tron</p>
                    <p>RTX</p>
                  </button>

                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="send"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/DashDASH.png" />
                    <p>Dash</p>
                    <p>DASH</p>
                  </button>
                </div>
              </div>

              <div class="card-width">
                <input class="welcome-section-exchange-input card-width" />
                <div class="welcome-section-exchange-input-after card-width">
                  <p>min.: 1 658.7241 USD</p>
                </div>
              </div>
            </div>

            <img
              class="welcome-section-exchange-arrows"
              src="../assets/img/arrows.png"
            />
            <span class="welcome-section-exchange-title mobile-element"
              >You receive</span
            >
            <div>
              <div
                id="currency_receive"
                class="welcome-section-exchange-items card-width"
              >
                <div id="selected_receive_curreny">
                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="receive"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/BitcoinBTC.png" />
                    <p>Bitcoin</p>
                    <p>BTC</p>
                  </button>
                </div>

                <div id="currency_receive_list" class="open-select hidden">
                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="receive"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/BitcoinBTC.png" />
                    <p>Bitcoin</p>
                    <p>BTC</p>
                  </button>

                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="receive"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/DaiDai.png" />
                    <p>Dai Dai</p>
                    <p></p>
                  </button>

                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="receive"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/TronTRX.png" />
                    <p>Tron</p>
                    <p>RTX</p>
                  </button>

                  <button
                    onclick="onClickCurrency(this)"
                    data-direction="receive"
                    class="currency-card-exchange card-width background-dark-grey white"
                  >
                    <img src="../assets/img/currency/DashDASH.png" />
                    <p>Dash</p>
                    <p>DASH</p>
                  </button>
                </div>
              </div>

              <div class="card-width">
                <input class="welcome-section-exchange-input card-width" />
                <div class="welcome-section-exchange-input-after card-width">
                  <p>min.: 0.1 BTC</p>
                  <p>Exchange rate: <b>100 USD = 0.00602873 BTC</b></p>
                  <p>Reserve: <b>68.7213 BTC</b></p>
                </div>
              </div>
            </div>
          </div>

          <div class="welcome-section-exchange-row">
            <div>
              <button
                @click="$router.push('/preview')"
                class="exchange-btn background-blue"
                type="button"
              >
                Exchange
              </button>
            </div>
          </div>
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

        <div class="welcome-bottom-block-arrow wide-descpot-element">
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

        <div class="welcome-bottom-block-arrow wide-descpot-element">
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

    <section class="info-section" id="info-section">
      <div class="info-block">
        <img src="../assets/img/clock.png" />
        <div class="info-text-block">
          <h2>Quick exchange</h2>
          <p>
            Operations on the service are carried out in automatic and
            semi-automatic mode, the guaranteed processing time of applications
            is 5-30 minutes
          </p>
        </div>
      </div>

      <div class="info-block">
        <div class="info-text-block">
          <h2>Reliable exchange</h2>
          <p>
            The service uses modern software, which guarantees the smooth
            operation of the site, reliability and security of transactions
          </p>
        </div>
        <img src="../assets/img/hands.png" />
      </div>

      <div class="info-block">
        <img src="../assets/img/circle.png" />
        <div class="info-text-block">
          <h2>Secure exchange</h2>
          <p>
            A full range of functions provides secure conversion of electronic
            currencies, a convenient service that does not cause questions and
            difficulties when performing an exchange
          </p>
        </div>
      </div>

      <div class="info-block">
        <div class="info-text-block">
          <h2>Minimum commissions</h2>
          <p>
            The exchange office provides services for the exchange of the most
            relevant directions: Bitcoin, Ethereum, Litecoin, Ripple, Monero,
            Cash, Qiwi, Yandex. Money, Alfa-Bank, Sberbank, Visa/Master, Exmoor
          </p>
        </div>
        <img src="../assets/img/wallet.png" />
      </div>

      <div class="info-block">
        <img src="../assets/img/medal.png" />
        <div class="info-text-block">
          <h2>Reputation and openness</h2>
          <p>
            The convenience and quality of the service is a priority for teams.
            We are always open to cooperation, please contact
          </p>
        </div>
      </div>

      <div class="info-block">
        <div class="info-text-block">
          <h2>Affiliate Program</h2>
          <p>
            Register on the site, share the link to the service with people and
            get rewarded with each exchange
          </p>
        </div>
        <img src="../assets/img/handshake.png" />
      </div>

      <div class="info-section-bottom-text">
        <p>
          Fast and reliable exchange of our customers. The guaranteed processing
          time of applications is 10-30 minutes after receiving the necessary
          number of confirmations. We do not require you to excess personal
          data.
        </p>
        <p>
          We will gladly exchange btc xmr zec dash eth usdt and any other coins.
          Write to us in an online chat - we will help you exchange large
          volumes of any coins!
        </p>
      </div>

      <div class="currency-cards-list">
        <button class="currency-card">
          <span class="currency-card-title">
            <img src="../assets/img/bitcoin.png" />
            <span>BTC/USDT</span>
          </span>
          <span>16237.90028</span>
        </button>

        <button class="currency-card">
          <span class="currency-card-title">
            <img src="../assets/img/bitcoin.png" />
            <span>BTC/USDT</span>
          </span>
          <span>16237.90028</span>
        </button>

        <button class="currency-card">
          <span class="currency-card-title">
            <img src="../assets/img/bitcoin.png" />
            <span>BTC/USDT</span>
          </span>
          <span>16237.90028</span>
        </button>

        <button class="currency-card">
          <span class="currency-card-title">
            <img src="../assets/img/bitcoin.png" />
            <span>BTC/USDT</span>
          </span>
          <span>16237.90028</span>
        </button>
      </div>
    </section>
  </div>
</template>

<script>
import('../assets/js/send_currency_select');
export default {};
</script>

<style scoped></style>
