import { Lote } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedeSocial";

export interface Evento {
  id: number;
  local:string ;
  dataEvento:Date;
  tema:string
  qtdePessoas:number;

  imagemURL:string ;

  telefone:string ;
  email:string ;
  lotes: Lote[];
  redessociais: RedeSocial[];
  palestranteeventos: Palestrante[];
}
