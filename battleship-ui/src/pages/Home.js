import React, { useEffect, useState } from 'react';
import Grid from '../components/Grid';
import { InputDialog } from '../components/InputDialog';
import { HttpClient } from "../utils/api-client"

const Home = () => {

    const [squaresA, setSquaresA] = useState(Array(10).fill(Array(10).fill(null)))
    const [squaresB, setSquaresB] = useState(Array(10).fill(Array(10).fill(null)))

    const [dataA, setDataA] = useState()
    const [dataB, setDataB] = useState()

    const [status, setStatus] = useState(0)

    useEffect(() => {
        fetchAndUpdateGame();
    }, []);

    function createNewGame(ships) {
        const data = { "ships": ships }
        HttpClient("battleship/start", { data })
            .then((result) => {
                const token = result.token
                localStorage.setItem("token", token)
                fetchAndUpdateGame();
            })
    }

    function onMark(x, y) {

        HttpClient(`battleship/mark/${x}/${y}`, { method: "POST" })
            .then((result) => {
                _updateGameData(result)
            })
    }

    function fetchAndUpdateGame() {
        HttpClient("battleship/status")
            .then((result) => {
                _updateGameData(result)
            })
    }

    function _updateGameData(result) {
        var gridA = result.boardA.grid
        var gridB = result.boardB.grid

        setDataA(result.boardA)
        setDataB(result.boardB)
        setStatus(result.gameStatus)

        let resultGrid = []
        for (var i = 0; i < 10; i++) {
            resultGrid.push(Array(10).fill(null))
        }

        for (var x of gridA) {
            resultGrid[Math.floor(x / 10)][x % 10] = 'X'
        }

        setSquaresA(resultGrid)

        resultGrid = []
        for (var i = 0; i < 10; i++) {
            resultGrid.push(Array(10).fill(null))
        }

        for (var x of gridB) {
            resultGrid[Math.floor(x / 10)][x % 10] = 'X'
        }

        setSquaresB(resultGrid)
    }

    let gameStatus = null;
    if (status === 1)
        gameStatus = "Game finished. You lost!"
    else if (status === 2)
        gameStatus = "Game finished. You won!"

    return (<React.Fragment>
        <InputDialog onCreateGame={createNewGame} />
        <div className="flex flex-row justify-center mt-5">
            <div className="mr-5">
                <Grid squares={squaresA}
                    setSquares={setSquaresA}
                    board={dataA} onMark={onMark}
                    isReadOnly={gameStatus !== null} />
            </div>
            <Grid squares={squaresB} isReadOnly={true}
                setSquares={setSquaresB}
                board={dataB} isReadOnly={true} isShowShips={true}></Grid>
        </div>
        {gameStatus && <div>{gameStatus}</div>}
    </React.Fragment>);
}

export default Home;
