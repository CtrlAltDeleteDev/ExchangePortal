const SEND = 'send';
const RECEIVE = 'receive';

let sendList = document.getElementById('currency_send_list');
let receiveList = document.getElementById('currency_receive_list');

let selected_send_curreny = document.getElementById('selected_send_curreny');
let selected_receive_curreny = document.getElementById(
  'selected_receive_curreny'
);

let currencySend = document.getElementById('currency_send');
let currencyReceive = document.getElementById('currency_receive');

currencySend.onclick = (e) => {
  toggleOpenSendList();
  console.log(e);
};

currencyReceive.onclick = (e) => {
  toggleOpenReceiveList();
  console.log(e);
};

function toggleOpenSendList() {
  const classList = sendList.classList;

  if (classList.contains('hidden')) {
    classList.remove('hidden');
  } else {
    classList.add('hidden');
  }
}

function toggleOpenReceiveList() {
  const classList = receiveList.classList;

  if (classList.contains('hidden')) {
    classList.remove('hidden');
  } else {
    classList.add('hidden');
  }
}

function onClickCurrency(element) {
  const direction = element.getAttribute('data-direction');

  const newEl = element.cloneNode(true);

  if (direction == SEND) {
    selected_send_curreny.innerHTML = '';
    selected_send_curreny.appendChild(newEl);
  }

  if (direction == RECEIVE) {
    selected_receive_curreny.innerHTML = '';
    selected_receive_curreny.appendChild(newEl);
  }
}
