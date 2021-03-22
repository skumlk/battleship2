import styled from "@emotion/styled";

const GridSquare = styled("div")`
  width: 30px;
  height: 30px;
  border: 1px solid black;
  display: flex;
  justify-content:center;
  align-items:center;
  user-select: none;
  -moz-user-select: none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  -o-user-select: none;
  background-color: ${props => props.isDestroyer ? 'hotpink' : props.isBattleship ? 'lightblue' : 'white'};
  color: ${props => props.isSuccessFire ? 'red' : 'black'};
  
`;

export { GridSquare }