export const GRID_SIZE = 10

export const ShipType = { DESTROYER: { id: 0, length: 4 }, BATTLESHIP: { id: 1, length: 5 } }

export function getShipLength(shipId){
    for(let x in ShipType){
        if(ShipType[x].id === shipId)
            return ShipType[x].length
    }

    return 0
}