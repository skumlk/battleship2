import {
    Button, FormLabel, Input, Modal, ModalBody,
    ModalCloseButton, ModalContent, ModalHeader, ModalOverlay, useDisclosure, ModalFooter
} from '@chakra-ui/react';
import React, { useState } from 'react';
import { Alert } from "@chakra-ui/react"
import { ShipType } from '../utils/const';
import { validateShipCoorindates } from '../utils/misc';
import { isIntersecting } from "../utils/misc"

export function InputDialog({ onCreateGame }) {

    const [warships, setWarShips] = useState({
        destroyer: { x: 0, y: 0, type: ShipType.DESTROYER.id },
        battleship1: { x: 0, y: 0, type: ShipType.BATTLESHIP.id },
        battleship2: { x: 0, y: 0, type: ShipType.BATTLESHIP.id }
    });

    const [error, setError] = useState({})
    const initialRef = React.useRef()
    const { isOpen, onOpen, onClose } = useDisclosure()

    function setValue(e, axis, type) {
        const value = parseInt(e.target.value)
        setError({})
        setWarShips(warships => {
            return { ...warships, [type]: { ...warships[type], [axis]: value } }
        })
    }

    function isValid() {

        let success = true;
        for (let key in warships) {
            let warship = warships[key]
            const { status, message } = validateShipCoorindates(warship)
            if (!status) {
                setError(error => ({ ...error, [key]: message }))
                success = false;
            }
        }

        if (isIntersecting(warships.destroyer, warships.battleship1)) {
            setError(error => ({ ...error, "global": "Destoyer and Battleship A are intersecting" }))
            success = false;
        }

        if (isIntersecting(warships.battleship1, warships.battleship2)) {
            setError(error => ({ ...error, "global": "Battleships are intersecting" }))
            success = false;
        }

        if (isIntersecting(warships.destroyer, warships.battleship2)) {
            setError(error => ({ ...error, "global": "Destoyer and Battleship B are intersecting" }))
            success = false;
        }

        return success;
    }

    function _createGame() {
        const result = []
        for (let key in warships)
            result.push({ ...warships[key] })

        if (!isValid()) return

        onClose();
        onCreateGame(result);
    }

    return (
        <>
            <Button onClick={onOpen} color="blue" className="mt-4">Start New Game</Button>
            <Modal
                initialFocusRef={initialRef}
                isOpen={isOpen}
                onClose={() => { onClose(); }}
            >
                <ModalOverlay />
                <ModalContent>
                    <ModalHeader>Enter battleship positions</ModalHeader>
                    <ModalCloseButton />
                    <ModalBody pb={6}>
                        <div className="d-flex flex-col mt-2">
                            <FormLabel>Destroyer</FormLabel>
                            <Input type="number" placeholder="X" onChange={(e) => setValue(e, 'x', 'destroyer')} />
                            <Input type="number" placeholder="Y" onChange={(e) => setValue(e, 'y', 'destroyer')} className="mt-1" />
                            {error.destroyer && <Alert>{error.destroyer}</Alert>}
                        </div>
                        <div className="d-flex flex-col mt-2">
                            <FormLabel>Battleship A</FormLabel>
                            <Input type="number" placeholder="X" onChange={(e) => setValue(e, 'x', 'battleship1')} />
                            <Input type="number" placeholder="Y" onChange={(e) => setValue(e, 'y', 'battleship1')} className="mt-1" />
                            {error.battleship1 && <Alert>{error.battleship1}</Alert>}
                        </div>
                        <div className="d-flex flex-col mt-2">
                            <FormLabel>Battleship B</FormLabel>
                            <Input type="number" placeholder="X" onChange={(e) => setValue(e, 'x', 'battleship2')} />
                            <Input type="number" placeholder="Y" onChange={(e) => setValue(e, 'y', 'battleship2')} className="mt-1" />
                            {error.battleship2 && <Alert>{error.battleship2}</Alert>}
                        </div>
                        <div className="mt-2">{error.global && <Alert>{error.global}</Alert>}</div>
                    </ModalBody>
                    <ModalFooter>
                        <Button colorScheme="blue" className="mr-3" onClick={_createGame}>Start New Game</Button>
                        <Button onClick={onClose}>Cancel</Button>
                    </ModalFooter>
                </ModalContent>
            </Modal>
        </>
    )
}