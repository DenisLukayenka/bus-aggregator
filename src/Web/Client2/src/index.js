import { GetValue } from './print'; 

console.log(GetValue());

function createElement() {
    const el = document.createElement('div');

    el.style.backgroundColor = 'red';
    el.innerHTML = 'Hello there!';

    return el;
}

document.body.appendChild(createElement());