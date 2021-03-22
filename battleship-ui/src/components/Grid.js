import React, { useState } from 'react';
import * as _ from "lodash";
import { GridSquare } from '../styles/style';

const RenderSquare = ({ value, x, y, handleClick, ships, successFire }) => {
    let isDestroyer = false
    let isBattleship = false
    if (ships) {
        for (let ship of ships) {
            const { type, startX, startY, endX, endY } = ship;
            if (x >= startX && x < endX && y >= startY && y <= endY) {
                if (type === "DESTROYER") isDestroyer = true
                if (type === "BATTLESHIP") isBattleship = true
                break;
            }
        }
    }

    let isSuccessFire = successFire && successFire.includes(x + y * 10)
    return <GridSquare isSuccessFire={isSuccessFire} isDestroyer={isDestroyer} isBattleship={isBattleship} key={x + y * 10} onClick={() => handleClick(x, y)} > {value}  </GridSquare>
}

const Grid = ({ isReadOnly, onMark, squares, setSquares, isShowShips, board = {} }) => {
    const { warShips, successFire } = board

    function handleClick(x, y) {

        if (isReadOnly) return

        setSquares((squares) => {
            const s = [...squares]
            s[y] = [...s[y]]
            s[y][x] = 'X'
            return s
        })

        if (onMark) onMark(x, y)
    }

    const warships = isShowShips && board["warShips"]

    return (
        <div>
            <div className="grid grid-cols-10">
                {squares.map((row, y) => {
                    return (row.map((cell, x) => <RenderSquare x={x} y={y}
                        handleClick={handleClick} value={squares[y][x]} ships={warships} successFire={successFire} />))
                })}
            </div>
            <div>{board.score}</div>
        </div>
    );
}

export default Grid;
