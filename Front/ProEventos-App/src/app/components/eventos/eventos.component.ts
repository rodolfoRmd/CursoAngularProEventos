
import { temporaryAllocator } from '@angular/compiler/src/render3/view/util';
import { Component, OnInit, TemplateRef } from '@angular/core';

import { Evento } from '../../models/Evento';
import { EventoService } from '../../services/evento.service';


import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public eventos: Evento[] =[];
  public eventosFiltrados: Evento[]=[];
  public widthImg: number=150;
  public marginImg:number=2;
  public mostrarImg = true;
  private _filtroLista:string='';
  modalRef?: BsModalRef;
  public nomeTitulo: string = "Eventos";


  public get filtroLista():string{
    return this._filtroLista;
  }

  public set filtroLista(value:string){
    this._filtroLista = value;
    this.eventosFiltrados  = this._filtroLista? this.filtraEventos(this._filtroLista):this.eventos;
  }

  public filtraEventos(filtrarPor:string):any{
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.eventos.filter(
      (evento:any)=>evento.tema.toLocaleLowerCase().indexOf(filtrarPor)!== -1 || evento.local.toLocaleLowerCase().indexOf(filtrarPor)!== -1
    );
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr : ToastrService,
    private spinner: NgxSpinnerService
    ) {}

 public  ngOnInit(): void {
    this.spinner.show();
    this.getEventos();



  }

  public alterarImagem(){
    this.mostrarImg = !this.mostrarImg;
  }

  public getEventos(): void {
    const observer = {
      next :(eventos: Evento[])=>{this.eventos = eventos;
        this.eventosFiltrados=this.eventos;},
      error:(erro:any)=>{
          console.log(erro);
          this.spinner.hide();
          this.toastr.error("Erro ao carregar eventos", "Erro")
        },
      complete: ()=>{this.spinner.hide();}

    }
    this.eventoService.getEventos().subscribe(
      observer
    );

  }


  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
   this.toastr.success("Sucesso ao deletar", 'Deletado');
    this.modalRef?.hide();
  }

  decline(): void {

    this.modalRef?.hide();
  }
}
