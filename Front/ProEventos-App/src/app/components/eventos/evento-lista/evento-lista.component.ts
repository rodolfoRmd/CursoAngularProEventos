
import { temporaryAllocator } from '@angular/compiler/src/render3/view/util';
import { Component, OnInit, TemplateRef } from '@angular/core';

import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';


import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  public eventos: Evento[] =[];
  public eventosFiltrados: Evento[]=[];
  public eventoId: number=0;
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
    private spinner: NgxSpinnerService,
    private router: Router
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


  openModal(event: any ,template: TemplateRef<any>, eventoId:number) {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.spinner.show();
    this.eventoService.deleteEvento(this.eventoId).subscribe(
      (result:any)=>{
         console.log(result);
          this.toastr.success("Sucesso ao deletar", 'Deletado');
          this.getEventos();

      },
      (error:any)=>{
        console.error(error);
        this.toastr.error("Erro ao tentar deletar o evento", 'Erro');

      },

      ).add(()=>this.spinner.hide());

    this.modalRef?.hide();
  }

  decline(): void {

    this.modalRef?.hide();
  }

  detalheEvento(id:number):void{
this.router.navigate([`eventos/detalhe/${id}`])
  }

}
