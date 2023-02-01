import React, { userState } from 'react';
import Header from './Header'

export default function App () {
  
  const [counter, setCounter] = userState(0);

  function Increase() {
    setCounter(counter + 1)
  }

  return (
    <div>
      <Header>
        Counter: {counter}
      </Header>
      <button onClick={Increase}>Add</button>
    </div>
  );
}