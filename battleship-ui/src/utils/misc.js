
import { getShipLength, GRID_SIZE, ShipType } from "./const"

export function isIntersecting(warship1, warship2) {

    if (warship1.y !== warship2.y) return false

    const xa1 = warship1.x
    const xa2 = xa1 + getShipLength(warship1.type)

    const xb1 = warship2.x
    const xb2 = xb1 + getShipLength(warship2.type)

    if (xb1 >= xa1 && xb1 <= xa2) return true;
    if (xb2 >= xa1 && xb2 <= xa2) return true;
    return false;
}

export function validateShipCoorindates(warship) {
    
    const { x, y, type } = warship
    
    if (x < 0 && y < 0) return { status: false, message: "Values should be larger than zero" }

    if (type === ShipType.DESTROYER.id && (x + ShipType.DESTROYER.length > GRID_SIZE))
        return { status: false, message: "Destroyer is out of range" }

    if (type === ShipType.BATTLESHIP.id && (x + ShipType.BATTLESHIP.length > GRID_SIZE))
        return { status: false, message: "Battleship is out of range" }

    return { status: true, message: null }
}