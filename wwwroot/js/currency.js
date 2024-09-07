const form = document.getElementById('exchange-form')

document.addEventListener('DOMContentLoaded', function () {

    stCurrency()
    
    form.addEventListener('submit', function (e) {
        e.preventDefault()
        calculate()
    })
    

    
})

async function calculate() {
    const currencyFrom = document.getElementById('from').value
    const currencyTo = document.getElementById('to').value
    const amount = document.getElementById('amount').value

    const response = await fetch(`/api/currencies/calcualte?fromCc=${currencyFrom}&toCc=${currencyTo}&amount=${amount}`)
    const data = await response.json()
    console.log(data)
    const result = document.getElementById('result')
    result.innerText = `Result: ${data.result}`
}

async function getCurrency() {
    const response = await fetch('/api/currencies')
    const data = await response.json()
    console.log(data)
    return data
}

async function stCurrency() {
    const data = await getCurrency()
    const currencyFrom = document.getElementById('from')
    const currencyTo = document.getElementById('to')
    
    
    data.forEach(currency => {
        const option = document.createElement('option')
        option.text = currency.text
        option.value = currency.cc
        
        const cFrom = option.cloneNode(true)
        const cTo = option.cloneNode(true)
        currencyFrom.add(cFrom)
        currencyTo.add(cTo)
    }
    )
}